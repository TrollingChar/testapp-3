using System;
using Core;


namespace Battle.Experimental {

    public class NewTimer {

        public event Action <Time> OnChanged;
        public event Action <bool> OnPauseChanged;
        
        
        public bool                Paused { get; set; }
        
        public bool Elapsed { get {throw new NotImplementedException();} }
        public int Seconds { get; set; }

        public void Wait () { throw new NotImplementedException ();}

        public void Wait (Time time) { throw new NotImplementedException (); }

        public void Update () { throw new NotImplementedException (); }

    }

}