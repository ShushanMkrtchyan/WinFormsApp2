﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public class Bot
    {
        public int[,] myMap = new int[Form1.mapSize, Form1.mapSize];// bot's map
        public int[,] enemyMap = new int[Form1.mapSize, Form1.mapSize];// player's map

        public Button[,] myButtons = new Button[Form1.mapSize, Form1.mapSize];
        public Button[,] enemyButtons = new Button[Form1.mapSize, Form1.mapSize];

        public Bot(int[,] myMap, int[,] enemyMap, Button[,] myButtons, Button[,] enemyButtons)
        {
            this.myMap = myMap;
            this.enemyMap = enemyMap;
            this.myButtons = myButtons;
            this.enemyButtons = enemyButtons;
        }

        public bool IsInsideMap(int i, int j)
        {
            if (i < 0 || j<0 || i > Form1.mapSize|| j > Form1.mapSize)
            {
                return false;
            }
            return true;
        }

        public bool IsEmpty(int i, int j,int length)
        {
            bool isEmpty = true; ;
            for(int k = j;k< j+length;k++)
            {
                if (myMap [i,j] !=0)
                {
                    isEmpty = false;
                    break;
                }
            }

            return isEmpty;
        }

        public int[,] ConfigurShips()
        {
            int lengthShip = 4;
            int cycleValue = 4;
            int shipsCount = 10;

            Random r = new Random();

            int posX = 0;
            int posY = 0;

            while(shipsCount>0)
            {
                for(int i = 0; i< cycleValue/4;i++)
                {
                    posX = r.Next(0, Form1.mapSize);
                    posY = r.Next(0, Form1.mapSize);

                    while(!IsInsideMap(posX,posY+lengthShip-1)|| (!IsEmpty(posX,posY,lengthShip)))
                    {
                        posX = r.Next(0, Form1.mapSize);
                        posY = r.Next(0, Form1.mapSize);
                    }

                    for(int k = posY;k<posY+lengthShip;k++)
                    {
                        myMap[posX, posY] = 1;
                    }
                    lengthShip--;
                    cycleValue *= 2;
                    shipsCount--;
                    if (shipsCount <= 0)
                        break;

                }
            }
            return myMap;
        }

      

        

        public bool Shoot() //mymap
        {
            bool hit = false;
            Random r = new Random();

            int posX = r.Next(1, Form1.mapSize);
            int posY = r.Next(1, Form1.mapSize);
            while (enemyButtons[posX, posY].BackColor == Color.Blue || enemyButtons[posX,posY].BackColor == Color.Black)

            {
                 posX = r.Next(1, Form1.mapSize);
                 posY = r.Next(1, Form1.mapSize);
            }

            if (enemyMap[posX,posY]!=0)
            {
                hit = true;
                enemyMap[posX, posY] = 0;
                enemyButtons[posX, posY].BackColor = Color.Blue;
                enemyButtons[posX, posY].Text = "X";
            }
            else
            {
                hit = false;
                enemyButtons[posX, posY].BackColor = Color.Black;
            }
            if (hit)
                Shoot();
            return hit;
        }
    }
}
