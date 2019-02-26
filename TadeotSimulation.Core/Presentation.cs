using System;
using System.Collections.Generic;

namespace TadeotSimulation.Core
{
    public class Presentation
    {
        private const int PRESENTATION_MINUTES = 10;

        #region Properties
        public int CountOfPeople { get; private set; }
        public DateTime EndOfPresentation { get; private set; }
        public int PresentationNumber { get; private set; }
        public DateTime StartOfPresentation { get; private set; }
        #endregion

        #region Methods
        public Presentation()
        {
        }

        #endregion
    }
}
