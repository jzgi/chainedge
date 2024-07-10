namespace ChainEdge.Drivers
{
    public class ESCPSerialPrintDriver : Driver
    {
        protected internal override void OnCreate(object state)
        {
        }

        public override void Rebind()
        {
        }

        
        public override string Label => "页打印";


        public void printBizlabel()
        {
        }

        public void PrintTitle(string v)
        {
        }

        public void PrintRow(short idx, string name, decimal price, short qty)
        {
        }

        public void PrintBottomLn()
        {
        }
    }
}