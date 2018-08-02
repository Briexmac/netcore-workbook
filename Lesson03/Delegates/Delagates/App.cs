using System;
using System.Collections.Generic;

namespace Delagates
{
    public class App
    {
        private readonly string[] _args;
        private readonly Parser _parser;
        private readonly Aggregator _aggregator;
        private readonly Saver _saver;

        private IEnumerable<object> _objects;
        private object _result;

        public Func<string[], IEnumerable<object>> ParseInput { get; set; }
        public Func<IEnumerable<object>, object> AggregateData { get; set; }
        public Action<object> SaveResult { get; set; }

        public App(string[] args)
        {
            _args = args;
            _parser = new Parser();
            _aggregator = new Aggregator();
            _saver = new Saver();
        }

        public void Run()
        {
            Parse();
            Aggregate();
            Save();
        }

        private void Save()
        {
            if (SaveResult != null)
                SaveResult(_result);
            else
                _saver.Save(_result);
        }

        private void Aggregate()
        {
            if (AggregateData != null)
                _result = AggregateData(_objects);
            else
                _result = _aggregator.Aggregate(_objects);
        }

        private void Parse()
        {
            if (ParseInput != null)
                _objects = ParseInput(_args);
            else
                _objects = _parser.Parse(_args);
        }
    }
}
