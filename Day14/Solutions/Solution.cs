using AOC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using AOC.Base.Helpers;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        List<BitMask> masks = new List<BitMask>();
        MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        protected override void DoGold()
        {
            ParseInput();
            PerfMon.Monitor("Calculate", () =>
            {
                var unknownMemory = new Dictionary<long, long>();

                foreach (var mask in masks)
                {
                    ApplyMask2(mask, unknownMemory);
                }

                var result = (long)0;
                foreach (var val in unknownMemory)
                {
                    result += val.Value;
                }

                Result = result;
            });
        }

        protected override void DoSilver()
        {
            PerfMon.Monitor("Parse", () =>
            {
                ParseInput();
            });


            PerfMon.Monitor("Calculate", () =>
            {
                var memory = new long[masks.SelectMany(x => x.ValueToMemoryPosition.Select(y => y.MemoryAddress)).Max()];

                foreach (var mask in masks)
                {
                    ApplyMask(mask, memory);
                }

                var result = (long)0;
                foreach (var val in memory)
                {
                    result += val;
                }

                Result = result;
            });
        }

        protected override void ParseInput()
        {
            masks = new List<BitMask>();
            var lines = ReadInput();
            BitMask mask = new BitMask();
            var cycle = 0;
            foreach (var line in lines)
            {
                var memoryAddress = Regex.Match(line, "^mem\\[([0-9]*)\\] = ([0-9]*)");
                var maskMatch = Regex.Match(line, "^mask = ([X01]*)");
                if (maskMatch.Success || cycle == lines.Length - 1)
                {
                    if (cycle == lines.Length - 1)
                    {
                        mask.ValueToMemoryPosition.Add(new Instruction
                        {
                            MemoryAddress = long.Parse(memoryAddress.Groups[1].Value),
                            Value = memoryAddress.Groups[2].Value
                        }); ;
                    }

                    if (!string.IsNullOrEmpty(mask.Mask))
                    {
                        masks.Add(mask);
                    }
                    mask = new BitMask();
                    mask.Mask = maskMatch.Groups[1].Value;
                } else if (memoryAddress.Success)
                {
                    mask.ValueToMemoryPosition.Add(new Instruction
                    {
                        MemoryAddress = long.Parse(memoryAddress.Groups[1].Value),
                        Value = memoryAddress.Groups[2].Value
                    }); ;
                }
                cycle++;
            }
        }

        private void ApplyMask(BitMask mask, long[] memory)
        {
            foreach (var instruction in mask.ValueToMemoryPosition)
            {
                var result = new StringBuilder();
                for (int i = 0; i < instruction.Value.Length; i++)
                {
                    switch (mask.Mask[i])
                    {
                        case 'X':
                        result.Append(instruction.Value[i]);
                        break;
                        case '0':
                        result.Append(0);
                        break;
                        case '1':
                        result.Append(1);
                        break;
                    }
                }

                memory[instruction.MemoryAddress - 1] = HelperMethods.ConvertBinaryFromString(result.ToString());
            }
        }

        private void ApplyMask2(BitMask mask, Dictionary<long, long> memory)
        {
            foreach (var instruction in mask.ValueToMemoryPosition)
            {
                var addressesToWrite = ApplyMaskToAddress(mask.Mask, instruction.MemoryAddress);

                foreach (var address in addressesToWrite)
                {
                    var addressAsNumber = HelperMethods.ConvertBinaryFromString(address);
                    if (!memory.ContainsKey(addressAsNumber))
                    {
                        memory.Add(addressAsNumber, 0);
                    }
                    memory[addressAsNumber] = HelperMethods.ConvertBinaryFromString(instruction.Value);
                }

            }
        }

        private List<string> ApplyMaskToAddress(string mask, long memoryAddress)
        {
            var startingAddress = HelperMethods.ConvertToBinary(memoryAddress.ToString());
            var result = new StringBuilder();
            for (int i = 0; i < mask.Length; i++)
            {
                switch (mask[i])
                {
                    case 'X':
                    result.Append('X');
                    break;
                    case '0':
                    result.Append(startingAddress[i]);
                    break;
                    case '1':
                    result.Append(1);
                    break;
                }
            }

            var listToReturn = new List<string>()
            {
                ""
            };

            foreach (var letter in result.ToString())
            {
                if (letter == 'X')
                {
                    var tempList = new List<string>();
                    foreach (var comb in listToReturn)
                    {
                        tempList.Add(comb + "0");
                        tempList.Add(comb + "1");
                    }
                    listToReturn = tempList;
                } else
                {
                    for (int i = 0; i < listToReturn.Count; i++)
                    {
                        listToReturn[i] += letter;
                    }
                }
            }

            return listToReturn;
        }
    }

    class BitMask
    {
        public string Mask { get; set; }
        public List<Instruction> ValueToMemoryPosition { get; set; } = new List<Instruction>();

    }

    class Instruction
    {
        public long MemoryAddress { get; set; }
        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = HelperMethods.ConvertToBinary(value);
            }
        }
    }
}
