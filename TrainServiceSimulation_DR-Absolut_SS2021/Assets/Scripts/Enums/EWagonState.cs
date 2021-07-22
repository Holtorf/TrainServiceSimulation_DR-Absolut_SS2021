namespace TrainServiceSimulation.Enums
{
    public enum EWagonState
    {
        /// <summary>
        /// WAgon has no state
        /// </summary>
        NONE,
        /// <summary>
        /// Wagon is waiting to get in a free service bay
        /// </summary>
        PENDING,
        /// <summary>
        /// Wagon is in a service bay
        /// </summary>
        WORKING,
        /// <summary>
        /// Wagon service ist finished and can get back to the trail
        /// </summary>
        COMPLETED
    }
}
