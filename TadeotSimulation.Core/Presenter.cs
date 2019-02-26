using System;
using System.Collections.Generic;
using System.Text;

namespace TadeotSimulation.Core
{
    class Presenter
    {
        #region Fields
        public event EventHandler<Presentation> PresentationStarted;
        public event EventHandler<Presentation> PresentationFinished;
        #endregion

        #region Properties
        public bool IsPresenting { get; set; }
        public string Name { get; private set; }
        public List<Presentation> Presentation { get;private set; }
        #endregion

        #region Methods
        public Presenter(string name)
        {
            Name = name;
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime currentTime)
        {
            throw new NotImplementedException();
        }

        public void StartPresentation()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
