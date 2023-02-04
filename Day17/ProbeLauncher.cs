using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day17
{
    internal class Probe
    {
        public (int x, int y) ProbePosition { get; set; }
        public (int x, int y) ProbeVelocity { get; set; }
        public int Apex { get; set; }

        List<(int x, int y)> _steps;

        public Probe((int x, int y) probeVelocity)
        {
            ProbePosition = (0, 0);
            ProbeVelocity = probeVelocity;
            _steps = new() { (0, 0) };
        }

        public void NextStep()
        {
            // calculate next point
            (int xPos, int yPos) = ProbePosition;
            (int xVel, int yVel) = ProbeVelocity;

            ProbePosition = (xPos + xVel, yPos + yVel);
            _steps.Add((xPos, yPos));

            // adjust x for drag, x moves toward 0
            if (xVel > 0)
                xVel -= 1;
            if (xVel < 0)
                xVel += 1;

            // adjust y for gravity, always decrements
            yVel -= 1;
            ProbeVelocity = (xVel, yVel);
        }

        public void PrintSteps()
        {
            foreach (var step in _steps)
            {
                Console.WriteLine($"{step.x}, {step.y}");
            }
            Console.WriteLine();
        }

        public int GetApex()
        {
            return _steps.Max(s => s.y);
        }
    }

    internal class ProbeLauncher
    {
        private (int x, int y) _lastPoint;
        private HashSet<(int x, int y)> _target;

        public int Apex { get; set; }
        public int Hits { get; set; }

        public ProbeLauncher(string line)
        {
            MatchCollection mc = Regex.Matches(line, @"-?\d+");
            int X1 = Convert.ToInt32(mc[0].Value);
            int X2 = Convert.ToInt32(mc[1].Value);

            // make sure Y1 is more positive
            int Y2 = Convert.ToInt32(mc[2].Value);
            int Y1 = Convert.ToInt32(mc[3].Value);

            //Console.WriteLine($"Target zone: x = {X1}..{X2}, y = {Y1}..{Y2}");

            _target = new();
            for (int x = X1; x <= X2; x++)
                for (int y = Y1; y >= Y2; y--)
                    _target.Add((x, y));

            _lastPoint = (X2, Y2);
        }

        bool TargetPassed(Probe pr)
        {
            if (pr.ProbePosition.x > _lastPoint.x || pr.ProbePosition.y < _lastPoint.y)
                return true;
            else
                return false;
        }

        bool TargetHit(Probe pr)
        {
            return _target.Contains(pr.ProbePosition);
        }

        public int FindTargetZone()
        {
            int x_st = 1;
            int x_sp = _lastPoint.x;
            int y_st = _lastPoint.y;
            int y_sp = 300;

            // try combinations of x and y velocities
            for (int y = y_st; y <= y_sp; y++)
            {
                for (int x = x_st; x <= x_sp; x++)
                {
                    Probe pr = new((x, y));

                    // step until it hits or passes the target
                    while ((TargetHit(pr) != true) && (TargetPassed(pr) != true))
                        pr.NextStep();

                    if (TargetHit(pr))
                    {
                        //pr.PrintSteps();
                        Apex = Math.Max(Apex, pr.GetApex());
                        Hits++;
                    }
                }
            }
            return Apex;
        }
    }

    
}
