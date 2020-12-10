using AOC.Base;
using System.Collections.Generic;
using System;
using System.Linq;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        GameConsole console = new GameConsole();
        protected override void DoGold()
        {

            ParseInput();
            console.Accumulator = 0;
            Result = console.ExecuteInstruction(0, new List<KeyValuePair<string, bool>>());
        }

        protected override void DoSilver()
        {
            ParseInput();
            Result = console.ExecuteInstruction2(0, new List<KeyValuePair<string, bool>>());
        }

        protected override void ParseInput()
        {
            var lines = ReadInput();

            console.Instructions = lines.Select(x => new KeyValuePair<string, bool>(x, false)).ToList();
        }
    }

    class GameConsole
    {
        public List<KeyValuePair<string, bool>> Instructions { get; set; } = new List<KeyValuePair<string, bool>>();
        public int Accumulator { get; set; }

        public int ExecuteInstruction(int instructionNumber, List<KeyValuePair<string, bool>> oldState, bool changed = false)
        {            
            if (instructionNumber >= Instructions.Count)
            {
                return Accumulator;
            }

            var instruction = Instructions[instructionNumber];

            if (instruction.Value == true)
            {
                Instructions = oldState;
                return 0;
            }

            Instructions[instructionNumber] = new KeyValuePair<string, bool>(instruction.Key, true);
            var command = instruction.Key.Substring(0, 3);
            var number = instruction.Key.Substring(4);

            switch (command)
            {
                case "acc":
                Accumulator += int.Parse(number);
                return ExecuteInstruction(instructionNumber+1, oldState, changed);
                case "jmp":
                // In case for backtracking
                var tempa = Accumulator;
                
                if (!changed)
                {
                    // change instruction for later stuff
                    oldState = Instructions.GetRange(0, Instructions.Count);
                    Instructions[instructionNumber] = new KeyValuePair<string, bool>("nop " + number, true);
                }

                var result = changed ? ExecuteInstruction(instructionNumber + int.Parse(number), oldState, true) : ExecuteInstruction(instructionNumber + 1, oldState, true);
                if (result == 0 && !changed)
                {
                    Accumulator = tempa;
                    return ExecuteInstruction(instructionNumber + int.Parse(number), oldState, false);
                };
                return result;
                case "nop":
                // In case for backtracking
                tempa = Accumulator;
                    
                if (!changed)
                {
                    // change instruction for later stuff
                    oldState = Instructions.GetRange(0, Instructions.Count);
                    Instructions[instructionNumber] = new KeyValuePair<string, bool>("jmp " + number, true);
                }
                
                result = changed ? ExecuteInstruction(instructionNumber + 1, oldState, true) : ExecuteInstruction(instructionNumber + int.Parse(number), oldState, true);
                if (result == 0 && !changed)
                {
                    Accumulator = tempa;
                    return ExecuteInstruction(instructionNumber + 1, oldState, false);
                };
                return result;
            }
            return 0;
        }

        public int ExecuteInstruction2(int instructionNumber, List<KeyValuePair<string, bool>> oldState, bool changed = false)
        {
            if (instructionNumber >= Instructions.Count)
            {
                return Accumulator;
            }
            var instruction = Instructions[instructionNumber];

            if (instruction.Value == true)
            {
                return Accumulator;
            }
            Instructions[instructionNumber] = new KeyValuePair<string, bool>(instruction.Key, true);
            var command = instruction.Key.Substring(0, 3);
            var number = instruction.Key.Substring(4);

            // reset instructions on failed attempt

            switch (command)
            {
                case "acc":
                Accumulator += int.Parse(number);
                return ExecuteInstruction2(instructionNumber + 1, oldState, changed);
                case "jmp":
                return ExecuteInstruction2(instructionNumber + int.Parse(number), oldState, false);
                case "nop":
                return ExecuteInstruction2(instructionNumber + 1, oldState, false);
            }
            return 0;
        }
    }
}
