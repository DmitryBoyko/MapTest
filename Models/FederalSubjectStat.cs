namespace MapTest.Models
{
    public sealed class FederalSubjectHeaderStat
    {
        public string FederalSubjectName { get; set; }
        public int ObjectTotal { get; set; }
        public int ObjectsInProcess { get; set; }
        public int ObjectsDone { get; set; }
        public int ProjectsTotal { get; set; }

        public BudjetStatItem BudjetStatItem { get; set; } = new BudjetStatItem();
        public PlansStatItem PlansStatItem { get; set; } = new PlansStatItem();
        public VariablesStatItem VariablesStatItem { get; set; } = new VariablesStatItem();
        public ControlPointStatItem ControlPointStatItem { get; set; } = new ControlPointStatItem();

    }
}
