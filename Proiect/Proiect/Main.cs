using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    public partial class Main : Form
    {
        private struct Line
        {
            public Point p1;
            public Point p2;
        }

        private struct Obstacol
        {
            public List<Point> puncte;
            public List<Line> linii;
        }

        private struct Triunghi
        {
            public Point A;
            public Point B;
            public Point C;
        }

        List<Line> listaLiniiSpatiu = new List<Line>();
        List<Point> listaPuncteSpatiu = new List<Point>();
        List<Line> listaLiniiObstacol = new List<Line>();
        List<Point> listaPuncteObstacol = new List<Point>();
        List<Obstacol> listaObstacole = new List<Obstacol>();
        List<Point> listaPuncteFolositeTriangulare = new List<Point>();
        List<Triunghi> listaTriunghiuri = new List<Triunghi>();
        Line currentLine;
        int nrPoints = 0;
        int nrPointsObs = 0;
        bool deseneazaSpatiuSauObstacole = false; //consideram false - spatiu , true = obstacole

        public Main()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if(!deseneazaSpatiuSauObstacole) //spatiu
            {
                nrPoints += 1;
                if (nrPoints == 1)
                {
                    currentLine.p1.X = e.X;
                    currentLine.p1.Y = e.Y;
                    listaPuncteSpatiu.Add(currentLine.p1);
                }
                else if (nrPoints == 2)
                {
                    currentLine.p2.X = e.X;
                    currentLine.p2.Y = e.Y;
                }
                else if (nrPoints > 2)
                {
                    var mP1 = currentLine.p2;
                    currentLine = new Line
                    {
                        //primul punct al noii linii este ultimul punct al liniei anterioare
                        p1 = mP1
                    };
                    //deseneaza direct al doilea punct al urmatoarei linii , din moment ce pe primul il avem din linia anterioara
                    currentLine.p2.X = e.X;
                    currentLine.p2.Y = e.Y;
                    listaLiniiSpatiu.Add(currentLine);
                    nrPoints = 2;
                }
            }
            else //obstacole
            {
                nrPointsObs += 1;
                if (nrPointsObs == 1)
                {
                    currentLine.p1.X = e.X;
                    currentLine.p1.Y = e.Y;
                    listaPuncteObstacol.Add(currentLine.p1);
                }
                else if (nrPointsObs == 2)
                {
                    currentLine.p2.X = e.X;
                    currentLine.p2.Y = e.Y;
                }
                else if (nrPointsObs > 2)
                {
                    var mP1 = currentLine.p2;
                    currentLine = new Line
                    {
                        //primul punct al noii linii este ultimul punct al liniei anterioare
                        p1 = mP1
                    };
                    //deseneaza direct al doilea punct al urmatoarei linii , din moment ce pe primul il avem din linia anterioara
                    currentLine.p2.X = e.X;
                    currentLine.p2.Y = e.Y;
                    listaLiniiObstacol.Add(currentLine);
                    nrPointsObs = 2;
                }
            }
           
            DrawPointsAndLines(currentLine);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            //redesenam liniile (impreuna cu punctele) ca sa adaugam ultimele modificari la ce exista deja
            if (listaLiniiSpatiu.Count > 0)
            {
                foreach (Line linie in listaLiniiSpatiu)
                {
                    DrawPointsAndLines(linie);
                }
            }
            else //daca nu exista inca nici o linie, inseamna ca trebuie sa desenam un punct (este primul)
            {
                if (currentLine.p1.X != 0 && currentLine.p1.Y != 0)
                    Canvas.CreateGraphics().FillRectangle(Brushes.Black, currentLine.p1.X, currentLine.p1.Y, 5,5);
            }
        }

        private void DrawPointsAndLines(Line line)
        {
            if(!deseneazaSpatiuSauObstacole) // spatiu
            {
                if (line.p1.X != 0 && line.p1.Y != 0)
                    Canvas.CreateGraphics().FillRectangle(Brushes.Black, line.p1.X, line.p1.Y, 5, 5);
                if (line.p1.X != 0 && line.p1.Y != 0 && line.p2.X != 0 && line.p2.Y != 0)
                {
                    //conditia aceasta verifica daca ultimul punct adaugat coincide cu ultimul, incat sa incercam sa inchidem forma
                    //verificarea se face astfel : daca coordonata X si Y se afla in intervalul punct initial.X sau Y + / - 10 
                    //inseamna ca dorim inchiderea formei
                    bool lastPointEqualsFirst = (line.p2.X >= listaPuncteSpatiu.ElementAt(0).X - 10 && line.p2.X <= listaPuncteSpatiu.ElementAt(0).X + 10) &&
                        (line.p2.Y >= listaPuncteSpatiu.ElementAt(0).Y - 10 && line.p2.Y <= listaPuncteSpatiu.ElementAt(0).Y + 10) ? true : false;
                    if (lastPointEqualsFirst)
                    {
                        //in cazul in care dorim sa inchidem forma, nu putem inchide fara nicio linie
                        bool closeShapeWithOneDot = (listaLiniiSpatiu.Count == 0) ? true : false;
                        if (!closeShapeWithOneDot)
                        {
                            //in cazul in care dorim sa inchidem forma, verificam ca nu avem doar o linie si sa incercam sa inchidem
                            bool closeShapeWithOneLine = (listaLiniiSpatiu.Count - 1 == 1) ? true : false;
                            if (!closeShapeWithOneLine)
                            {
                                //Cand se adauga ultimul punct, doar se deseneaza, pentru ca altfel ar exista un punct in plus
                                listaLiniiSpatiu.Add(currentLine);
                                Canvas.CreateGraphics().DrawLine(new Pen(Brushes.Black), line.p1, line.p2);
                                MessageBox.Show("Forma s-a inchis cu success!");
                                //resetam datele curente
                                currentLine = new Line
                                {
                                    p1 = new Point(0, 0),
                                    p2 = new Point(0, 0)
                                };
                                deseneazaSpatiuSauObstacole = true; //trecem la obstacole odata ce am terminat cu forma
                            }
                            else
                            {
                                MessageBox.Show("Forma nu se poate inchide cu o singura linie!");
                                //Nu s-a finalizat creearea liniei, revenim la desenarea celui de-al doilea punct
                                nrPoints = 1;
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Forma nu se poate inchide cu un singur punct!");
                            //Nu s-a finalizat creearea primei linii, revenim la desenarea celui de-al doilea punct
                            nrPoints = 1;
                            Canvas.Invalidate();
                            return;

                        }
                    }
                    else
                    //Punctul nu este de inchidere deci doar continuam cu adaugarea si desenarea punctului si liniei
                    {
                        bool closeOnItself = (line.p2.X >= listaPuncteSpatiu.ElementAt(listaPuncteSpatiu.Count - 1).X - 10 && line.p2.X <= listaPuncteSpatiu.ElementAt(listaPuncteSpatiu.Count - 1).X + 10) &&
                        (line.p2.Y >= listaPuncteSpatiu.ElementAt(listaPuncteSpatiu.Count - 1).Y - 10 && line.p2.Y <= listaPuncteSpatiu.ElementAt(listaPuncteSpatiu.Count - 1).Y + 10) ? true : false;
                        if (!closeOnItself)
                        {
                            listaPuncteSpatiu.Add(currentLine.p2);
                            listaLiniiSpatiu.Add(currentLine);
                            Canvas.CreateGraphics().FillRectangle(Brushes.Black, line.p2.X, line.p2.Y, 5, 5);
                            Canvas.CreateGraphics().DrawLine(new Pen(Brushes.Black), line.p1, line.p2);
                        }
                        else
                        {
                            MessageBox.Show("Forma nu poate ramane deschisa!");
                            //Nu s-a finalizat creearea liniei, revenim la desenarea celui de-al doilea punct
                            nrPoints = 1;
                            return;
                        }
                    }
                }
            }
            else //obstacole (aproape algoritm identic, doar ce se adauga in alte liste punctele si liniiile
            //TODO : Optimizare - poate
            {
                if (line.p1.X != 0 && line.p1.Y != 0)
                    Canvas.CreateGraphics().FillRectangle(Brushes.Red, line.p1.X, line.p1.Y, 5, 5);
                if (line.p1.X != 0 && line.p1.Y != 0 && line.p2.X != 0 && line.p2.Y != 0)
                {
                    //conditia aceasta verifica daca ultimul punct adaugat coincide cu ultimul, incat sa incercam sa inchidem forma
                    //verificarea se face astfel : daca coordonata X si Y se afla in intervalul punct initial.X sau Y + / - 10 
                    //inseamna ca dorim inchiderea formei
                    bool lastPointEqualsFirst = (line.p2.X >= listaPuncteObstacol.ElementAt(0).X - 10 && line.p2.X <= listaPuncteObstacol.ElementAt(0).X + 10) &&
                        (line.p2.Y >= listaPuncteObstacol.ElementAt(0).Y - 10 && line.p2.Y <= listaPuncteObstacol.ElementAt(0).Y + 10) ? true : false;
                    if (lastPointEqualsFirst)
                    {
                        //in cazul in care dorim sa inchidem forma, nu putem inchide fara nicio linie
                        bool closeShapeWithOneDot = (listaLiniiObstacol.Count == 0) ? true : false;
                        if (!closeShapeWithOneDot)
                        {
                            //in cazul in care dorim sa inchidem forma, verificam ca nu avem doar o linie si sa incercam sa inchidem
                            bool closeShapeWithOneLine = (listaLiniiObstacol.Count - 1 == 1) ? true : false;
                            if (!closeShapeWithOneLine)
                            {
                                //Idem spatiu, nu se adauga ultimul punct in lista, doar se deseneaza linia
                                listaLiniiObstacol.Add(currentLine);
                                Canvas.CreateGraphics().DrawLine(new Pen(Brushes.Red), line.p1, line.p2);
                                MessageBox.Show("Obstacolul s-a inchis cu success!");
                                listaObstacole.Add(new Obstacol
                                {
                                    puncte = listaPuncteObstacol,
                                    linii = listaLiniiObstacol
                                });
                                //resetam datele curente
                                currentLine = new Line
                                {
                                    p1 = new Point(0, 0),
                                    p2 = new Point(0, 0)
                                };
                                nrPointsObs = 0;
                                Canvas.CreateGraphics().FillPolygon(Brushes.Red, listaPuncteObstacol.ToArray()); //umplem obstacolul cu rosu
                                listaPuncteObstacol = new List<Point>();
                                listaLiniiObstacol = new List<Line>();

                            }
                            else
                            {
                                MessageBox.Show("Obstacolul nu se poate inchide cu o singura linie!");
                                //Nu s-a finalizat creearea liniei, revenim la desenarea celui de-al doilea punct
                                nrPointsObs = 1;
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Obstacolul nu se poate inchide cu un singur punct!");
                            //Nu s-a finalizat creearea primei linii, revenim la desenarea celui de-al doilea punct
                            nrPointsObs = 1;
                            return;

                        }
                    }
                    else
                    //Punctul nu este de inchidere deci doar continuam cu adaugarea si desenarea punctului si liniei
                    {
                        bool closeOnItself = (line.p2.X >= listaPuncteObstacol.ElementAt(listaPuncteObstacol.Count - 1).X - 10 && line.p2.X <= listaPuncteObstacol.ElementAt(listaPuncteObstacol.Count - 1).X + 10) &&
                        (line.p2.Y >= listaPuncteObstacol.ElementAt(listaPuncteObstacol.Count - 1).Y - 10 && line.p2.Y <= listaPuncteObstacol.ElementAt(listaPuncteObstacol.Count - 1).Y + 10) ? true : false;
                        if (!closeOnItself)
                        {
                            listaPuncteObstacol.Add(currentLine.p2);
                            listaLiniiObstacol.Add(currentLine);
                            Canvas.CreateGraphics().FillRectangle(Brushes.Red, line.p2.X, line.p2.Y, 5, 5);
                            Canvas.CreateGraphics().DrawLine(new Pen(Brushes.Red), line.p1, line.p2);
                        }
                        else
                        {
                            MessageBox.Show("Obstacolul nu poate ramane deschis!");
                            //Nu s-a finalizat creearea liniei, revenim la desenarea celui de-al doilea punct
                            nrPointsObs = 1;
                            return;
                        }
                    }
                }
            }
        }

        //Inloc sa gasim algoritmi complicati de triangularizare, consideram asa :
        //Un triunghi este format din trei puncte distincte, unite intre ele 
        //Deci trebuie sa gasim acele puncte si sa le unim. Prin urmare vom lua fiecare punct al fiecarui obstacol
        //Vom cauta 2 dintre cele mai apropiate puncte din multimea punctelor spatiului
        //Si vom forma un triunghi cu acestea
        //Conditia este sa nu existe alt punct / forma intre oricare doua puncte ale triunghiului (ideal)
        //Asa putem da o idee de rezolvare, ptr ca proful a zis ca oricum nu toate cazurile vor merge
        //Cazul de pseudo-functionare la noi este : Daca vrei x obstacole cu n laturi , trebuie sa ai n+1 puncte in spatiu

        private void TriangulateBttn_Click(object sender, EventArgs e)
        {
            DrawLineBetweenClosestPointsForAllObstacles();
        }

        private void DrawLineBetweenClosestPointsForAllObstacles()
        {
            foreach (Obstacol o in listaObstacole)
            {
                foreach(Point p in o.puncte)
                {
                    DrawLineBetweenClosestPoints(p);
                }
            }
        }

        private void DrawLineBetweenClosestPoints(Point reference)
        {
            Tuple<Point, Point> pereche = FindClosestPoints(reference, listaPuncteSpatiu);
            Canvas.CreateGraphics().DrawLine(Pens.Green, reference, pereche.Item1);
            Canvas.CreateGraphics().DrawLine(Pens.Green, reference, pereche.Item2);
            Canvas.CreateGraphics().DrawLine(Pens.Green, pereche.Item1, pereche.Item2);
            Triunghi t;
            t.A = reference;
            t.B = pereche.Item1;
            t.C = pereche.Item2;
            listaTriunghiuri.Add(t);
        }

        private Tuple<Point,Point> FindClosestPoints(Point reference, List<Point> list)
        {
            double distMinP1 = Double.MaxValue;
            double distMinP2 = Double.MaxValue;
            Point p1 = new Point(); ;
            Point p2 = new Point();

            //Este nevoie de o lista auxiliara pentru a nu deteriora continutul listei originale cu punctele spatiului
            List<Point> listaAuxiliara = new List<Point>();
            foreach (Point p in listaPuncteSpatiu)
            {
                listaAuxiliara.Add(p);
            }

            foreach (Point p in listaAuxiliara)
            {
                //calculam distanta dintre punctul nostru si punctul din spatiu
                double distanta = Math.Pow((reference.X - p.X), 2) + Math.Pow((reference.Y - p.Y), 2);
                //recalculam minimul si retinem care este punctul cu pricina din spatiu
                if(distanta < distMinP1)
                {
                    //Nu permitem sa se foloseasca puncte deja gasite, deoarece asta inseamna ca incalecam forma deja existenta
                    if(listaPuncteFolositeTriangulare.Count == 0)
                    {
                        distMinP1 = distanta;
                        p1 = p;
                    }
                    else
                    //Deci daca exista deja puncte folosite, cautam sa nu avem un punct care indeplineste conditia de distanta
                    //Dar a fost deja folosit
                    //Daca nu este cazul, se merge mai departe
                    {
                        bool exists = false;
                        foreach(Point point in listaPuncteFolositeTriangulare)
                        {
                            if(point.Equals(p))
                            {
                                exists = true;
                            }
                        }
                        if(!exists)
                        {
                            distMinP1 = distanta;
                            p1 = p;
                        }
                    }
                }
            }

            //La finalul cautarii eliminam punctul din lista punctelor din spatiu si il cautam pe al doilea
            listaAuxiliara.Remove(p1);

            foreach (Point p in listaAuxiliara)
            {
                //calculam distanta dintre punctul nostru si punctul din spatiu
                double distanta = (Math.Sqrt(Math.Pow(Math.Abs(reference.X - p.X), 2) + Math.Pow(Math.Abs(reference.Y - p.Y), 2)));
                //recalculam minimul si retinem care este punctul cu pricina din spatiu
                if(distanta < distMinP2)
                {
                    if(listaPuncteFolositeTriangulare.Count == 0)
                    {
                        distMinP2 = distanta;
                        p2 = p;
                    }
                    else
                    {
                        bool exists = false;
                        foreach(Point point in listaPuncteFolositeTriangulare)
                        {
                            if(point.Equals(p))
                            {
                                exists = true;
                            }
                        }
                        if(!exists)
                        {
                            distMinP2 = distanta;
                            p2 = p;
                        }
                    }
                }
            }

            //Returnam cele doua puncte cu care vom desena triunghiul
            Tuple<Point, Point> result = new Tuple<Point, Point>(p1, p2);
            listaPuncteFolositeTriangulare.Add(p1);
            return result;
        }

        private void deleteBttn_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
