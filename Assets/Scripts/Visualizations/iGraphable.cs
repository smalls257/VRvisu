using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Visualizations
{
    public interface iGraphable
    {
        //See documentation for implementation details
        void createShell();

        void saveVisualization();

        void reloadVisualization();

        void resizeVisualization();

        void selectVisualization();

        //void plotData();
    }
}
