using System;
using System.Linq;
using System.Collections.Generic;
namespace Turing
{
    class State
    {
        public string start;
        public string end;
        public string replace;
        public string read;
        public string move;
        public State(string _end, string _start, string _replace, string _read, string _move)
        {
            start = _start;
            end = _end;
            replace = _replace;
            read = _read;
            move = _move;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            List<string> tape = new List<string>();
            //δ (q1,a) = (q2,b, L)
            //1 01 011 011 01
            Dictionary<string, List<State>> mach = new Dictionary<string, List<State>>();
            string key = "", end = "", replace = "", read = "", move = "";
            //bool key_flag , terminal_flga = false;
            //key_flag = true;
            string finish = "";
            for (int i = 0; i < input.Length; i++)
            {
                try
                {
                    for (; input[i] != '0'; i++)
                    {
                        key = key + input[i];
                    }
                    i++;
                    //for (; input[i] != '0'; i++)
                    //{
                    //    start = start + input[i];
                    //}
                    //i++;
                    for (; input[i] != '0'; i++)
                    {
                        read = read + input[i];
                    }
                    i++;
                    for (; input[i] != '0'; i++)
                    {
                        end = end + input[i];
                    }
                    i++;
                    for (; input[i] != '0'; i++)
                    {
                        replace = replace + input[i];
                    }
                    i++;
                    for (; input[i] != '0'; i++)
                    {
                        move = move + input[i];
                    }
                    i++;
                }
                catch (IndexOutOfRangeException)
                {

                }
                
                if (mach.ContainsKey(key))
                {
                    State sTemp = new State(end, key, replace, read, move);
                    //List<State> temp = new List<State>();
                    mach[key].Add(sTemp);
                }
                else
                {
                    State sTemp = new State(end, key, replace, read, move);
                    List<State> temp = new List<State>();
                    temp.Add(sTemp);
                    mach.Add(key, temp);
                }
                if (!mach.ContainsKey(end))
                
                {
                    if (input.Length  == i)
                    {
                        finish = end;
                    }
                    List<State> temp = new List<State>();
                    mach.Add(end, temp);
                }
                key = ""; end = ""; move = ""; read = ""; replace = "";
            }
            int count = int.Parse(Console.ReadLine());
            tape.Clear();
            tape.Add("1");
            tape.Add("1");
            for (int i = 0; i < count; i++)
            {
                string str = Console.ReadLine();
                
                for (int j = 0; j < str.Length; j++)
                {
                    string temp = "";
                    try
                    {
                        for (; str[j] == '1'; j++)
                        {
                            temp = temp + str[j];
                        }
                    }
                    catch (IndexOutOfRangeException) { }
                    
                    tape.Insert(1, temp);
                }
                tape.Reverse();
                Automata(mach, tape, finish);
                tape.Clear();
                tape.Add("1");
                tape.Add("1");
            }
        }
        static bool Automata(Dictionary<string, List<State>> mach, List<string> tape, string end)
        {
            int index = 1;// point to tape
            string state = "1";
            while (true)
            {
                bool flag = false;
                try
                {
                    for (int j = 0; j < mach[state].Count; j++)
                    {
                        if (mach[state][j].read == tape[index])
                        {
                            tape[index] = mach[state][j].replace;
                            if (mach[state][j].move == "1")
                            {
                                index--;
                                if (index < 0)
                                {
                                    index++;
                                    tape.Insert(0, "1");
                                }
                                
                            }
                            else
                            {
                                index++;
                                if (index >= tape.Count)
                                {
                                    tape.Add("1");
                                }
                            }
                            state = mach[state][j].end;
                            flag = true;
                            if (state == end)
                            {
                                Console.WriteLine("Accepted");
                                return true;
                            }
                            break;
                        }
                        if (state == end)
                        {
                            Console.WriteLine("Accepted");
                            return true;
                        }

                    }
                    if (!flag)
                    {
                        Console.WriteLine("Rejected");
                        return false;
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Rejected");
                    return false;
                    throw;
                }
                
            }
            //if (state == end)
            //{
            //    Console.WriteLine("Accepted");
            //    return true;
            //}
            //else
            //    return false;
        }
    }
    
}
