using System;

namespace Ohce
{
    public class Ohce
    {
        private IConsole _console;
        private string _name;
        private DateTime _time;
        private bool _greetingShown;
        public bool Finished { get; private set; }

        public Ohce(string name, DateTime time, IConsole @console)
        {
            if (console == null) throw new ArgumentNullException(nameof(console));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            _name = name;
            _time = time;
            _console = console;
        }

        public void Run()
        {
            if (!_greetingShown)
            {
                ShowGreeting();
                _greetingShown = true;
            }
            var r = _console.ReadLine();
            ProcessInput(r);
        }

        private void ProcessInput(string r)
        {
            if (IsPalindrome(r))
            {
                OnPalindrome(r);
                return;
            }

            if (IsExitCommand(r))
            {
                OnExitCommand();
                return;
            }

            _console.Write(Reverse(r));
        }

        private void OnExitCommand()
        {
            _console.Write("Adios " + _name);
            Finished = true;
        }

        private void OnPalindrome(string r)
        {
            _console.Write(Reverse(r));
            _console.Write("¡Bonita palabra!");
            return;
        }

        private void ShowGreeting()
        {
            if (IsItNight(_time))
                _console.Write("Buenas noches " + _name);
            else if (ItIsDay(_time))
                _console.Write("Buenos días " + _name);
            else if (ItIsAfternoon(_time))
                _console.Write("Buenas tardes " + _name);
        }

        private bool IsPalindrome(string r)
        {
            return r == Reverse(r);
        }

        private static bool IsExitCommand(string r)
        {
            return r == "Stop!";
        }

        private string Reverse(string inputString)
        {
            char[] charArray = inputString.ToCharArray();
            Array.Reverse(charArray);

            string reversed = new string(charArray);
            return reversed;
        }

        private bool ItIsAfternoon(DateTime time)
        {
            return time.TimeOfDay >= new TimeSpan(12, 0, 0) && time.TimeOfDay < new TimeSpan(20, 0, 0);
        }

        private bool ItIsDay(DateTime time)
        {
            return time.TimeOfDay >= new TimeSpan(6, 0, 0) && time.TimeOfDay < new TimeSpan(12, 0, 0);
        }

        private bool IsItNight(DateTime time)
        {
            return time.TimeOfDay >= new TimeSpan(22, 0, 0) && time < time.AddHours(8);
        }
    }

}
