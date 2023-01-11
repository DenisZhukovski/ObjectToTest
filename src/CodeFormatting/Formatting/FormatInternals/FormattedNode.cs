using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class FormattedNode
    {
        private readonly Item _item;
        private readonly Tabs _parentTabs;
        private readonly IFormatLogger _logger;
        private readonly NodeFormatResults _results;
        private readonly List<INodeFormat> _formats;
        private readonly List<INodeTransformation> _transformations;

        public FormattedNode(
            Item item,
            Tabs parentTabs,
            IFormatLogger logger,
            NodeFormatResults results,
            List<INodeFormat> formats,
            List<INodeTransformation> transformations
        )
        {
            _item = item;
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
            IFormatLogger logger) : this(new Item(item), new Tabs(0), logger, new NodeFormatResults(), formats, transformations)
        {
        }

        public string String
        {
            get
            {
                _logger.WriteLine($"{new Tabs(_item.Depth)}{_item}: Processing...");

                var result = new NodeFormatResult(_item.Value, _results);

                if (result.AlreadyCalculated)
                {
                    _logger.WriteLine($"{new Tabs(_item.Depth + 1)}Already processed.");

                    return result.String;
                }

                var format = new ApplicableFormat(_item.Value, _formats);

                if (format.IsAvailable)
                {
                    var transformations = new ApplicableTransformations(_item.Value, _transformations);

                    var (formatTemplate, tabs) = format.ToApply.Apply(_item.Value, _parentTabs);

                    _logger.WriteLine($"{new Tabs(_item.Depth + 1)}Format [{new FormatName(format.ToApply)}]-[" + new NewLineAsN(formatTemplate) + "] applied.");

                    foreach (var nodeFormat in format.Ignored)
                    {
                        _logger.WriteLine($"{new Tabs(_item.Depth + 1)}Format [{new FormatName(nodeFormat)}] skipped - overriden.");
                    }

                    foreach (var transformationAndCondition in transformations)
                    {
                        (formatTemplate, tabs) = transformationAndCondition.Apply(formatTemplate, tabs);

                        _logger.WriteLine($"{new Tabs(_item.Depth + 1)}Transformation [{new TransformationName(transformationAndCondition)}]-[" + new NewLineAsN(formatTemplate) + "] applied.");
                    }

                    var formattedArguments = format.ToApply.Args(_item.Value).Select((x, index) => ChildNode(x, tabs, index).String).ToArray();

                    return new FormattedString(
                        formatTemplate,
                        formattedArguments
                    ).ToString();
                }

                result.String = _item.Value.ToString();

                _logger.WriteLine($"{new Tabs(_item.Depth + 1)}{_item.Value.GetType().Name}: format not found, use ToString()");

                return result.String;
            }
        }

        public FormattedNode ChildNode(object arg, Tabs tabs, int argIndex)
        {
            return new FormattedNode(
                new Item(arg, _item.Depth + 1, $"{{{argIndex}}}"),
                tabs,
                _logger,
                _results,
                _formats,
                _transformations
            );
        }
    }
}