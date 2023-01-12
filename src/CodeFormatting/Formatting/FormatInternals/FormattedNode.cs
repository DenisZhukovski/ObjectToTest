using System.Collections.Generic;
using System.Linq;
using ObjectToTest.Infrastructure;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class FormattedNode
    {
        private readonly FormatArgument _argument;
        private readonly Tabs _parentTabs;
        private readonly ILogger _logger;
        private readonly NodeFormatResults _results;
        private readonly List<INodeFormat> _formats;
        private readonly List<INodeTransformation> _transformations;

        public FormattedNode(
            FormatArgument argument,
            Tabs parentTabs,
            ILogger logger,
            NodeFormatResults results,
            List<INodeFormat> formats,
            List<INodeTransformation> transformations
        )
        {
            _argument = argument;
            _parentTabs = parentTabs;
            _logger = logger;
            _results = results;
            _formats = formats;
            _transformations = transformations;
        }

        public FormattedNode(
            object item,
            List<INodeFormat> formats,
            List<INodeTransformation> transformations,
            ILogger logger) : this(new FormatArgument(item), new Tabs(0), logger, new NodeFormatResults(), formats, transformations)
        {
        }

        public string String
        {
            get
            {
                var logger = new LoggerWithIndentation(_logger, new Tabs(_argument.Depth));

                logger.WriteLine($"{_argument}: Processing...");

                logger.Tab();

                var result = new NodeFormatResult(_argument.Value, _results);

                if (result.AlreadyCalculated)
                {
                    logger.WriteLine($"Already processed.");

                    return result.String;
                }

                var format = new ApplicableFormat(_argument.Value, _formats);

                if (format.IsAvailable)
                {
                    var transformations = new ApplicableTransformations(_argument.Value, _transformations);

                    var (formatTemplate, tabs) = format.ToApply.Apply(_argument.Value, _parentTabs);

                    logger.WriteLine($"Format [{new FormatName(format.ToApply)}]-[" + new NewLineAsN(formatTemplate) + "] applied.");

                    foreach (var nodeFormat in format.Ignored)
                    {
                        logger.WriteLine($"Format [{new FormatName(nodeFormat)}] SKIPPED - overriden.");
                    }

                    foreach (var transformationAndCondition in transformations)
                    {
                        (formatTemplate, tabs) = transformationAndCondition.Apply(formatTemplate, tabs);

                        logger.WriteLine($"Transformation [{new TransformationName(transformationAndCondition)}]-[" + new NewLineAsN(formatTemplate) + "] applied.");
                    }

                    var formattedArguments = format.ToApply.Args(_argument.Value).Select((x, index) => ChildNode(x, tabs, index).String).ToArray();

                    return new FormattedString(
                        formatTemplate,
                        formattedArguments
                    ).ToString();
                }

                result.String = _argument.Value.ToString();

                logger.WriteLine($"{_argument.Value.GetType().Name}: format not found, use ToString()");

                return result.String;
            }
        }

        public FormattedNode ChildNode(object arg, Tabs tabs, int argIndex)
        {
            return new FormattedNode(
                new FormatArgument(arg, _argument.Depth + 1, $"{{{argIndex}}}"),
                tabs,
                _logger,
                _results,
                _formats,
                _transformations
            );
        }
    }
}