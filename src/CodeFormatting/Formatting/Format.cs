using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjectToTest.CodeFormatting.Formatting.Core;
using ObjectToTest.Infrastructure;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class Format : IFormat
    {
        private readonly List<INodeFormat> _conditionalFormats = new();
        private readonly List<INodeTransformation> _conditionalTransformations = new();
        private readonly ILogger _logger;

        public Format(ILogger? logger = null)
        {
            _logger = logger ?? new SilentLogger();
        }

        public void Add(INodeFormat format)
        {
            _conditionalFormats.Add(format);
        }

        public void AddAsFirst(INodeFormat format)
        {
            _conditionalFormats.Insert(0, format);
        }

        public void AddAsFirst(INodeTransformation transformation)
        {
            _conditionalTransformations.Insert(0, transformation);
        }

        public void Add(INodeTransformation transformation)
        {
            _conditionalTransformations.Add(transformation);
        }

        public string ApplyTo(object item)
        {
            _logger.WriteLine($"{item.GetType().Name}: begin formatting");
            
            var result = new FormattedNode(item, _conditionalFormats, _conditionalTransformations, _logger).String;

            _logger.WriteLine($"{item.GetType().Name}: end formatting");

            return result;
        }
    }

}