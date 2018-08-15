namespace Anarchy {
        ///<summary>
        ///Represents the likelihood that a call will fail or succeed.
        ///</summary>
        public enum Entropy {
        ///<summary>
        ///Inconvient calls fail 25% of the time.
        ///</summary>
        Inconvience,
        ///<summary>
        ///Disorder calls will fail 50% of the time.
        ///</summary>
        Disorder,
        ///<summary>
        ///Vortex of chaos calls fail 75% of the time.
        ///</summary>
        VortexOfChaos,
        ///<summary>
        ///End Times calls fail 99% of the time.
        ///</summary>
        EndTimes
    }
}