using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TadeotSimulation.Core
{
    public class Controller
    {
        private const int MIN_PEOPLE_PER_PRESENTATION = 10;
        private const int MAX_PEOPLE_PER_PRESENTATION = 20;
        private const int MAX_WAITING_MINUTES = 40;

        private List<Visitor> _listOfVisitors;
        private List<Visitor> _waitingVisitors;
        private List<Presenter> _presenters;

        private int _countOfAllVisitors;
        private List<Visitor> _finishedVisitors;

        public event EventHandler<string> Log;


        public Controller()
        {
        }

        /// <summary>
        /// Besucher werden aus der csv-Datei in den Arbeitsvorrat eingelesen
        /// </summary>
        public void ReadVisitorsFromCsv()
        {
            string fileName = Utils.MyFile.GetFullNameInApplicationTree("Visitors.csv");
            string[] lines = File.ReadAllLines(fileName, Encoding.Default);
            string[] columns;
            for (int i = 1; i < lines.Length; i++)
            {
                columns = lines[i].Split(';');
                int id = int.Parse(columns[0]);
                int adults = int.Parse(columns[3]);
                DateTime entryDateAndTime = DateTime.Parse(columns[1] + " " + columns[2]);
                Visitor visitor = new Visitor(id, adults, entryDateAndTime);
                _listOfVisitors.Add(visitor);
            }
            Log?.Invoke(this, $"Read {_listOfVisitors.Count} visitors with {_listOfVisitors.Sum(s => s.Adults)} adults from csv-file");
        }

        /// <summary>
        /// Die Simulation startet 60 Minuten, bevor der erste Besucher laut csv-Datei kommt.
        /// Der Beschleunigungsfaktor wird auf 6000 gesetzt.
        /// </summary>
        public void StartSimulation()
        {
            FastClock.Instance.Factor = 6000;
            DateTime timeToStart = _listOfVisitors.Select(s => s.EntryTime).Min().AddMinutes(-60);
            FastClock.Instance.Time = timeToStart;
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
           // Presentation.Instance.PresentationFinished += Instance_PresentationFinished;
            FastClock.Instance.IsRunning = true;
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime currentTime)
        {
            OnOneMinuteIsOver(currentTime);
        }


        /// <summary>
        /// Checks the sum of visitors and when the minimum of people per presentation is reached,
        /// the presentation will start
        /// </summary>
        /// <param name="time"></param>
        public void OnOneMinuteIsOver(DateTime time)
        {
            _waitingVisitors = _listOfVisitors.Where(w => w.EntryTime <= time).ToList();
           // _countVisitors = _waitingVisitors.Count;
            _countOfAllVisitors = _waitingVisitors.Count + _waitingVisitors.Sum(s => s.Adults);

            if (
                _waitingVisitors.Count + _waitingVisitors.Sum(s => s.Adults) >= MIN_PEOPLE_PER_PRESENTATION
              //  && _presentationIsFinished
              //  || _lastPresentationFinished.AddMinutes(MAX_WAITING_MINUTES) == time
                && _waitingVisitors.Count > 0
                )
            {
                foreach (Visitor visitor in _waitingVisitors)
                {
                    _listOfVisitors.Remove(visitor);
                }
              //  Presentation.Instance.StartPresentation(_waitingVisitors);
            }
        }
    }
}
