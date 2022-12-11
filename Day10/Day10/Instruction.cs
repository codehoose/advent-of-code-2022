namespace Day10
{
    internal class Instruction
    {
        public Command command;

        public int param;

        public static Instruction MakeNoop() => new Instruction { command = Command.Noop };

        public static Instruction MakeFetchX(int value) => new Instruction { command = Command.FetchX, param = value };

        public static Instruction MakeLoadX(int value) => new Instruction { command = Command.LoadX, param = value };
    }
}
