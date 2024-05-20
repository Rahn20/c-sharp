namespace WildlifeTrackerSystem.src
{
    /// <summary>
    ///   Class serves as schedule for feeding of the animals.
    /// </summary>
    public class FoodSchedule
    {
        private EaterType eaterType;
        private ListManager<string> food;      // ex: morning: ***, lunch: ***, evening: ***

        public FoodSchedule()
        {
            food = new ListManager<string>();
        }

        public ListManager<string> Food
        {
            get { return food; }
        }

        public EaterType EaterType
        {
            get { return eaterType; }
            set { eaterType = value; }
        }
       

        public new string ToString()
        {
            return string.Format("{0, -20} {1, 10}", "Eastertype:", eaterType);
        }
    }
}
