namespace Day10
{
    /*
     * Program:

        noop
        addx 3
        addx -5

     */

    /*

        CYCLE_START
        noop -> No operation                    X = 1
        CYCLE_END, cycle ++

        CYCLE_START
        load command addr                       X = 1
        CYCLE_END, cycle ++

        CYCLE_START
        execute command                         X = 4
        CYCLE_END, cycle ++

     */

    /*
        At the start of the first cycle, the noop instruction begins execution. During the first cycle, X is 1.
        After the first cycle, the noop instruction finishes execution, doing nothing.

        At the start of the second cycle, the addx 3 instruction begins execution. During the second cycle, 
        X is still 1.

        During the third cycle, X is still 1. After the third cycle, the addx 3 instruction finishes execution, 
        setting X to 4.

        At the start of the fourth cycle, the addx -5 instruction begins execution. During the fourth cycle, 
        X is still 4.

        During the fifth cycle, X is still 4. After the fifth cycle, the addx -5 instruction finishes execution, 
        setting X to -1.
     */

    internal class Machine
    {
        Instruction _instruction = null;
        private int _fetchIndex = 0;
        private int _x = 1;
        private int _cycle = 1;
        private string[] _memory;

        public int X => _x;

        public int Cycle => _cycle;

        public bool IsRunning => _fetchIndex < _memory.Length || _instruction != null;

        public Machine(string[] memory)
        {
            _memory = memory;
        }

        public void Tick()
        {
            if (_instruction == null)
            {
                _instruction = FetchInstruction();
            }

            if (_instruction.command == Command.Noop)
            {
                _instruction = null;
                _cycle++;
            }
            else if (_instruction.command == Command.FetchX)
            {
                _instruction = Instruction.MakeLoadX(_instruction.param);
                _cycle++;
            }
            else
            {
                _x += _instruction.param;
                _instruction = null;
                _cycle++;
            }
        }

        private Instruction FetchInstruction()
        {
            string line = _memory[_fetchIndex++];

            if (line == "noop")
            {
                return Instruction.MakeNoop();
            }

            return Instruction.MakeFetchX(int.Parse(line.Split(" ".ToCharArray())[1]));
        }
    }
}
