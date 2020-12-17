using AOC.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        Dictionary<(int, int, int), bool> _cubeStates;
        Dictionary<(int, int, int,int), bool> _cubeStates2;
        protected override void DoGold()
        {
            ParseInput();
            for (int cycle = 0; cycle < 6; cycle++)
            {
                AddExtraRows2();
                _cubeStates2 = CheckNeighbours(cycle, _cubeStates2);
            }

            Result = _cubeStates2.Select(x => x.Value).Count(x => x);
        }

        protected override void DoSilver()
        {
            ParseInput();
            for (int cycle = 0; cycle < 6; cycle++)
            {
                AddExtraRows();
                _cubeStates = CheckNeighbours(cycle,_cubeStates);
            }

            Result = _cubeStates.Select(x => x.Value).Count(x => x);
        }
        #region Silver
        private void AddExtraRows()
        {
            var xMax = _cubeStates.Select(x => x.Key.Item1).Max();
            var xMin = _cubeStates.Select(x => x.Key.Item1).Min();

            var yMax = _cubeStates.Select(x => x.Key.Item2).Max();
            var yMin = _cubeStates.Select(x => x.Key.Item2).Min();

            var zMax = _cubeStates.Select(x => x.Key.Item3).Max();
            var zMin = _cubeStates.Select(x => x.Key.Item3).Min();
            for (int i = xMin-1; i <= xMax+1; i++)
            {
                for (int j = zMin; j <= zMax; j++)
                {
                    _cubeStates.Add((i,yMin-1,j), false);
                    _cubeStates.Add((i,yMax+1,j), false);
                }
            }

            for (int i = yMin; i <= yMax; i++)
            {
                for (int j = zMin; j <= zMax; j++)
                {
                    _cubeStates.Add((xMin-1,i,j), false);
                    _cubeStates.Add((xMax+1,i,j), false);
                }
            }

            xMax = _cubeStates.Select(x => x.Key.Item1).Max();
            xMin = _cubeStates.Select(x => x.Key.Item1).Min();

            yMax = _cubeStates.Select(x => x.Key.Item2).Max();
            yMin = _cubeStates.Select(x => x.Key.Item2).Min();

            for (int i = yMin; i <= yMax; i++)
            {
                for (int j = xMin; j <= xMax; j++)
                {
                    _cubeStates.Add((j,i,zMax+1), false);
                    _cubeStates.Add((j,i,zMin-1), false);
                }
            }
        }
        private Dictionary<(int, int, int), bool> CheckNeighbours(int cycle, Dictionary<(int, int, int), bool> cubeStates)
        {
            var returnState = new Dictionary<(int, int, int), bool>();
            foreach (var state in cubeStates)
            {
                var activeNeighbours = 0;
                for (int z = -1; z <= 1; z++)
                {
                        activeNeighbours += CheckSpecificNeighbours(returnState, 0, 0, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState, 1, 0, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState, -1, 0, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState, 1, 1, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState, -1, 1, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState, 0, 1, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState, 1, -1, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState, -1, -1, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState, 0, -1, z, state, cubeStates);

                }
                if (state.Value)
                {
                    activeNeighbours--;
                }
                if (state.Value && (activeNeighbours != 2 && activeNeighbours != 3))
                {
                    returnState.Add(state.Key, !state.Value);
                } else
                if (!state.Value && activeNeighbours == 3)
                {
                    returnState.Add(state.Key, !state.Value);
                } else
                {
                    returnState.Add(state.Key, state.Value);
                }
            }

            return returnState;
        }
        private int CheckSpecificNeighbours(Dictionary<(int, int, int), bool> returnState, int x, int y, int z, KeyValuePair<(int,int,int), bool> state, Dictionary<(int, int, int), bool> cubeStates)
        {
            var result = 0;
            if (cubeStates.TryGetValue((state.Key.Item1 + x, state.Key.Item2 + y, state.Key.Item3 + z), out bool cubeState))
            {
                if (cubeState)
                    result++;
            }
            return result;
        }

        #endregion
        #region Gold
        private void AddExtraRows2()
        {
            var xMax = _cubeStates2.Select(x => x.Key.Item1).Max();
            var xMin = _cubeStates2.Select(x => x.Key.Item1).Min();
                                  
            var yMax = _cubeStates2.Select(x => x.Key.Item2).Max();
            var yMin = _cubeStates2.Select(x => x.Key.Item2).Min();
                                  
            var zMax = _cubeStates2.Select(x => x.Key.Item3).Max();
            var zMin = _cubeStates2.Select(x => x.Key.Item3).Min();
                                  
            var wMax = _cubeStates2.Select(x => x.Key.Item4).Max();
            var wMin = _cubeStates2.Select(x => x.Key.Item4).Min();
            for (int i = xMin-1; i <= xMax+1; i++)
            {
                for (int j = zMin; j <= zMax; j++)
                {
                    for (int w = wMin; w <= wMax; w++)
                    {
                        _cubeStates2.Add((i, yMin - 1, j,w), false);
                        _cubeStates2.Add((i, yMax + 1, j,w), false);
                    }
                }
            }

            for (int i = yMin; i <= yMax; i++)
            {
                for (int j = zMin; j <= zMax; j++)
                {
                    for (int w = wMin; w <= wMax; w++)
                    {
                        _cubeStates2.Add((xMin - 1, i, j,w), false);
                        _cubeStates2.Add((xMax + 1, i, j,w), false);
                    }
                }
            }

            xMax = _cubeStates2.Select(x => x.Key.Item1).Max();
            xMin = _cubeStates2.Select(x => x.Key.Item1).Min();

            yMax = _cubeStates2.Select(x => x.Key.Item2).Max();
            yMin = _cubeStates2.Select(x => x.Key.Item2).Min();

            for (int i = yMin; i <= yMax; i++)
            {
                for (int j = xMin; j <= xMax; j++)
                {
                    for (int w = wMin; w <= wMax; w++)
                    {
                        _cubeStates2.Add((j, i, zMax + 1,w), false);
                        _cubeStates2.Add((j, i, zMin - 1,w), false);
                    }
                }
            }
            
            zMax = _cubeStates2.Select(x => x.Key.Item3).Max();
            zMin = _cubeStates2.Select(x => x.Key.Item3).Min();
            
            for (int i = yMin; i <= yMax; i++)
            {
                for (int j = xMin; j <= xMax; j++)
                {

                    for (int w = zMin; w <= zMax; w++)
                    {
                        _cubeStates2.Add((j, i, w, wMin-1), false);
                        _cubeStates2.Add((j, i, w, wMax+1), false);
                    }
                }
            }
        }
        private Dictionary<(int, int, int, int), bool> CheckNeighbours(int cycle, Dictionary<(int, int, int,int), bool> cubeStates)
        {
            var returnState = new Dictionary<(int, int, int, int), bool>();
            foreach (var state in cubeStates)
            {
                var activeNeighbours = 0;
                for (int z = -1; z <= 1; z++)
                {
                    for (int w = -1; w <= 1; w++)
                    {
                        activeNeighbours += CheckSpecificNeighbours(returnState,w , 0, 0, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState,w , 1, 0, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState,w , -1, 0, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState,w , 1, 1, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState,w , -1, 1, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState,w , 0, 1, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState,w , 1, -1, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState,w , -1, -1, z, state, cubeStates);
                        activeNeighbours += CheckSpecificNeighbours(returnState,w , 0, -1, z, state, cubeStates);

                    }
                    
                }
                if (state.Value)
                {
                    activeNeighbours--;
                }
                if (state.Value && (activeNeighbours != 2 && activeNeighbours != 3))
                {
                    returnState.Add(state.Key, !state.Value);
                } else
                if (!state.Value && activeNeighbours == 3)
                {
                    returnState.Add(state.Key, !state.Value);
                } else
                {
                    returnState.Add(state.Key, state.Value);
                }
            }

            return returnState;
        }
        private int CheckSpecificNeighbours(Dictionary<(int, int, int,int), bool> returnState, int w, int x, int y, int z, KeyValuePair<(int,int,int,int), bool> state, Dictionary<(int, int, int,int), bool> cubeStates)
        {
            var result = 0;
            if (cubeStates.TryGetValue((state.Key.Item1+x, state.Key.Item2+y, state.Key.Item3+z, state.Key.Item4 + w), out bool cubeState))
            {
                if (cubeState)
                    result++;
            }
            return result;
        }
        #endregion


        protected override void ParseInput()
        {
            _cubeStates = new Dictionary<(int, int, int), bool>();
            _cubeStates2 = new Dictionary<(int, int, int,int), bool>();
            var startState = ReadInput();
            for (int i = 0; i < startState.Length; i++)
            {
                for (int j = 0; j < startState[i].Length; j++)
                {
                    _cubeStates.Add((j, i, 0), startState[i][j] == '#');
                }
            }
            for (int i = 0; i < startState.Length; i++)
            {
                for (int j = 0; j < startState[i].Length; j++)
                {
                    _cubeStates2.Add((j, i, 0,0), startState[i][j] == '#');
                }
            }
        }
    }
}
