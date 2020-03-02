using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp43
{
    public class Randomizer
    {
        Random r = new Random();
        public int returnbin()
        {
            return r.Next(2);
        }
    }

    
    public class Obiekt
    {
        Randomizer mi;
        public double probability = 0,probability2=0;
        double[] tabx;
        public int[] longdna;
        public double[] tabxx;
        public double wynik=0;
        public double wynik2=0;
        public int[,] dna;
        public int aa,bb,mm,nn,il,templdwsk;
        double sum = 0;
        public void transformdna()
        {
            System.Buffer.BlockCopy(dna, 0, longdna, 0, mm * nn * 4);
        }
        public void printlongdna()
        {
            for(int lngcat = 0; lngcat < longdna.Length; lngcat++)
            {
                //Console.Write(longdna[lngcat]+" | ");
                
            }
            //Console.WriteLine();
            //Console.WriteLine();
        }
        public void printshortdna()
        {
            for (int j = 0; j < nn; j++)
            {
                for (int k = 0; k < mm; k++)
                {
                    Console.Write(dna[j, k]);
                    
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }
        public void updateshortdna()
        {
            templdwsk = 0;
            for (int j = 0; j < nn; j++)
            {
                for (int k = 0; k < mm; k++)
                {
                    dna[j, k] = longdna[templdwsk];
                    templdwsk++;
                }
            }
        }
        public Obiekt()
        {
            mi = new Randomizer();
        }
        public Obiekt(Obiekt walicto, int ilo, int n, int a, int b, int m)
        {
            mi = new Randomizer();
            int licz=walicto.longdna.Length;
            longdna = new int[licz];
            for (int walict = 0; walict < walicto.longdna.Length; walict++)
            {
                longdna[walict] = (int)walicto.longdna[walict];
            }
            il = ilo;
            aa = a;
            probability2 = walicto.probability2;
            bb = b;
            mm = m;
            nn = n;
            tabx = new double[n];
            tabxx = new double[n];
            
            dna = new int[n, mm];
            updateshortdna();
            findxp();

        }
        public Obiekt(Randomizer xd,int ilo,int n, int a, int b, int m)
        {

            il = ilo;
            aa = a;
            
            bb = b;
            mm = m;
            nn = n;
            tabx = new double[n];
            tabxx = new double[n];
            longdna = new int[mm * n];
            dna = new int[ n,mm];
            for (int j = 0; j < n; j++)
            {
                for(int k = 0; k < mm; k++)
                {
                    dna[j, k] = xd.returnbin();
                    //Console.Write(dna[j, k] + "  ");
                }
                //Console.WriteLine();
            }
            
            transformdna();
            findxp();
        }
      
        public void findxp()
        {

            for(int g = 0; g < nn; g++)
            {
                sum = 0;
                for (int h = dna.GetLength(1)-1; h>0; h--)
                {
                    sum += dna[g,h] * Math.Pow(2, h);
                }
                tabx[g] = sum;
                tabxx[g] =Math.Round( aa + (((bb - aa) * tabx[g]) / (Math.Pow(2, mm) - 1)),6);
                //Console.WriteLine(tabxx[g]);
            }
        }
        public void countprob(double f,double ff)
        {

            probability = wynik / f;
            probability2 = wynik / f;
            //Console.WriteLine( "  wynik " +wynik+ "/f " + f+"wynosi "+probability2);
            probability += ff;
            //Console.WriteLine(probability+"  Prawdopodobienstwo 2    "+probability2);
        }
            public void count()
        {
            
            for (int g = 0; g < nn; g++)
            {
                //Console.Write(tabxx[g]+"   ");
            }
            wynik = il * nn;
            for(int se = 0; se < tabxx.Length; se++)
            {
                wynik += Math.Pow(tabxx[se],2) - il * Math.Cos(20 * Math.PI * tabxx[se]);
            }
            //Console.WriteLine("Wynik: "+wynik);
        }
    }
    public class Populacja
    {
        public double mutationval = 0.1,invval=0.5;
        public Randomizer xd = new Randomizer();
        public double wynikipopulacji;
        public Random xdd = new Random();
        public List<int> tournament = new List<int>();
        int il = 0,aaa,nnn,mmm,bbb;
        public List<Double> allscores = new List<double>();
        public List<Double> allscoressorted = new List<double>();
        public double maxfitness = 0;
        double calkowitedopasowanie = 0;
        double calkowitedopasowanie2 = 0;
        double dystrybuanta = 0;
        public Populacja prevpopulation;
        public List<Obiekt> obiektypop = new List<Obiekt>();
        public List<Obiekt> newgeneration = new List<Obiekt>();
        public List<Obiekt> uneditedparents = new List<Obiekt>();
        public List<Obiekt> parentstheme = new List<Obiekt>();
        public List<Obiekt> beczka = new List<Obiekt>();
        public List<Obiekt> beczka2 = new List<Obiekt>();
        public List<Obiekt> afterinv = new List<Obiekt>();
        public List<Obiekt> mergedall = new List<Obiekt>();
        public List<Obiekt> aftermut = new List<Obiekt>();
        public List<Obiekt> tempobiektypop = new List<Obiekt>();
       
        public void partialselection()
        {
            obiektypop = parentstheme.ToList();
            mergedall = aftermut.Concat(afterinv).ToList();
            mergedall = mergedall.Concat(beczka).ToList();
            for (int fps = 0; fps < obiektypop.Count; fps++)
            {
                if (xdd.NextDouble() < 0.5)
                {
                    newgeneration.Add(obiektypop[xdd.Next(obiektypop.Count)]);
                }
                else
                {
                    newgeneration.Add(mergedall[xdd.Next(mergedall.Count)]);
                }
            }
            obiektypop = newgeneration.ToList();
        }
        public void randomselection()
        {
            obiektypop = parentstheme.ToList();
            mergedall = aftermut.Concat(afterinv).ToList();
            mergedall = mergedall.Concat(beczka).ToList();
            mergedall = mergedall.Concat(obiektypop).ToList();
            for (int fps = 0; fps < obiektypop.Count; fps++)
            {
               newgeneration.Add(mergedall[xdd.Next(mergedall.Count)]);
            }
            Console.WriteLine("ATENTION " + mergedall.Count);
            obiektypop = newgeneration.ToList();
        }

        public void eliteselection()
        {
            obiektypop = parentstheme.ToList();
            mergedall = aftermut.Concat(afterinv).ToList();
            mergedall = mergedall.Concat(beczka).ToList();
            mergedall = mergedall.Concat(obiektypop).ToList();
            mergedall = mergedall.OrderBy(a => a.probability2).ToList();
            mergedall.Reverse();
            for (int fps = 0; fps < obiektypop.Count; fps++)
            {
                //Console.WriteLine("Wybieram " + mergedall[fps].probability2);
                newgeneration.Add(mergedall[fps]);
            }
            obiektypop = newgeneration.ToList();

        }
        public void wholeselection()
        {
            
            mergedall = obiektypop.Concat(beczka).ToList();
            
        
            for (int fps = 0; fps < il; fps++)
            {
               
                    newgeneration.Add(mergedall[xdd.Next(mergedall.Count)]);
               
            }
            obiektypop = newgeneration.ToList();
           
        }
        public void prepareforcrossing()
        {
            //uneditedparents = parentstheme.ToList();
            
            for (int tt = 0; tt < obiektypop.Count; tt++)
            {
                if (xdd.NextDouble() < 0.5)
                {
                    beczka.Add(new Obiekt(obiektypop[tt],il,nnn,aaa,bbb,mmm));
                    //beczka2.Add(obiektypop[tt]);
                    //Console.WriteLine("ELEMENT BECZKI");
                    //beczka[beczka.Count-1].printlongdna();
                    
                }
                
            }
            if (beczka.Count % 2 == 1)
            {
                beczka.RemoveAt(beczka.Count - 1);
                //beczka2.RemoveAt(beczka.Count - 1);
                
            }
            
            //beczka= beczka.OrderBy(a => Guid.NewGuid()).ToList();
            //Console.WriteLine(beczka.Count);
        }
        public void evencrossing()
        {
            prepareforcrossing();
            int tempval;
            List<int> theme = new List<int>();
            if (beczka.Count > 0)
            {
                int dnalength = beczka[0].longdna.Count();
                //Console.WriteLine("Wzorzec:");
                for (int ec = 0; ec<dnalength;ec++)
                {
                    theme.Add(xdd.Next(2));
                    Console.Write(theme[theme.Count - 1] + " | ");
                    
                }
                Console.WriteLine();
                for (int tt = 0; tt < beczka.Count - 1; tt += 2)
                {
                    //Console.WriteLine("Dna rodzica:");
                    //beczka[tt].printlongdna();
                    //Console.WriteLine("Dna rodzica:");
                    beczka[tt + 1].printlongdna();
                    for (int tt2 = 0; tt2 < dnalength; tt2++)
                    {
                        if (theme[tt2] == 0)
                        {
                         
                        }
                        else if(theme[tt2] == 1)
                        {
                            tempval = beczka[tt].longdna[tt2];
                            beczka[tt].longdna[tt2] = beczka[tt + 1].longdna[tt2];
                            beczka[tt + 1].longdna[tt2] = tempval;
                            //beczka2[tt].longdna[tt2] = beczka2[tt + 1].longdna[tt2];
                            //beczka2[tt + 1].longdna[tt2] = beczka2[tt].longdna[tt2];

                        }
                    }

                    //Console.WriteLine("Dna dziecka:");
                    //beczka[tt].printlongdna();
                    //Console.WriteLine("Dna dziecka:");
                    beczka[tt + 1].printlongdna();
                    beczka[tt].updateshortdna();
                    beczka[tt + 1].updateshortdna();
                    //beczka2[tt].updateshortdna();
                    //beczka2[tt + 1].updateshortdna();

                }
               
            }
            uneditedparents = parentstheme.ToList();
        }
        public void crossingpoints(int crossingval)
        {

            int tempvalforp;
            int tempdnavalu2;
            int temporaryparent = 0;
            int dnalength;
            prepareforcrossing();
            if (beczka.Count > 0)
            {
                //Console.Clear();
                //Console.WriteLine(beczka.Count + " " + beczka2.Count);
                dnalength = beczka[0].longdna.Count()-1;
                List<int> cpoints = new List<int>();
               
                cpoints.Add(xdd.Next(dnalength));
                for (int tt = 0; tt < crossingval-1; tt++)
                {
                    cpoints.Add(xdd.Next(dnalength-cpoints[cpoints.Count-1])+ cpoints[cpoints.Count - 1]);
                }
                //Console.WriteLine(cpoints.Count);
                //beczka.Count - 1
                for (int tt = 0; tt <2; tt += 2)
                {
                    
                    //Console.WriteLine("Dna rodzica:");
                    //beczka[tt].printlongdna();
                    //Console.WriteLine("Dna rodzica:");
                    //beczka[tt+1].printlongdna();
                    //Console.WriteLine("Punkty przeciecia:");
                    for(int tyt = 0; tyt < cpoints.Count; tyt++)
                    {
                        //Console.Write(cpoints[tyt]+", ");

                    }
                    //Console.WriteLine();
                    int tempdnavalu;
                    

                    for (int tt2 = 0; tt2 <= dnalength; tt2++)
                    {
                        
                        if (temporaryparent != 0)
                        {

                            tempvalforp = beczka[tt].longdna[tt2];
                            beczka[tt].longdna[tt2] = beczka[tt + 1].longdna[tt2];
                            beczka[tt + 1].longdna[tt2] = tempvalforp;
                        }
                        
                        if (cpoints.Contains(tt2))
                        {
                            temporaryparent = 1 - temporaryparent;
                            //Console.WriteLine("RODZIC " + temporaryparent);
                        }

                    }
                    temporaryparent = 0;
                    //Console.WriteLine("Dna dziecka:");
                    //beczka[tt].printlongdna();
                    //Console.WriteLine("Dna dziecka:");
                    //beczka[tt + 1].printlongdna();

                    beczka[tt].updateshortdna();
                    beczka[tt+1].updateshortdna();
                    beczka2 = beczka.ToList();

                    //Console.WriteLine(tt+" "+beczka.Count);
                }
            }
        }
        public void calculateskillset()
        {
            for (int i = 0; i < il; i++)
            {
                if (prevpopulation.obiektypop[i].wynik>maxfitness)
                {
                    maxfitness = prevpopulation.obiektypop[i].wynik;
                }
            }
            for (int i = 1; i < il; i++)
            {
                prevpopulation.obiektypop[i].wynik2 =  maxfitness - prevpopulation.obiektypop[i].wynik + 1;
            }
            for (int i = 1; i < il; i++)
            {
                calkowitedopasowanie2 += prevpopulation.obiektypop[i].wynik2;
            }
            for (int i = 1; i < il; i++)
            {
                prevpopulation.obiektypop[i].countprob(calkowitedopasowanie2, prevpopulation.obiektypop[i - 1].probability);
            }
        }
        public void calculateprobability()
        {
            for (int i = 0; i < il; i++)
            {
                calkowitedopasowanie += prevpopulation.obiektypop[i].wynik;
            }
            for (int i = 1; i < il; i++)
            {

                prevpopulation.obiektypop[i].countprob(calkowitedopasowanie, prevpopulation.obiektypop[i - 1].probability);
            }
        }
        //Selekcja i zapełnianie populacji: Metoda 1 Ruletka
        public void selectchildrenm1()
        {
            
            for (int i = 0; i < il; i++)
            {
                dystrybuanta += prevpopulation.obiektypop[i].probability;
            }
            
            for (int i = 0; i < il; i++)
            {
                double ods = xdd.NextDouble();
                if (ods < prevpopulation.obiektypop[1].probability)
                {
                    
                    obiektypop.Add(prevpopulation.obiektypop[0]);  
                }
                else
                {
                    int el = 0;
                    for(int j = 1; j < il; j++)
                    {
                        if (ods> prevpopulation.obiektypop[j].probability)
                        {
                            el++;
                        }
                    }
                    obiektypop.Add(prevpopulation.obiektypop[el]);
                }
                Console.WriteLine("Obiekt "+obiektypop[i].wynik);
            }
        }
        public void selectchildrenm1min()
        {
            calculateskillset();
            for (int i = 0; i < il; i++)
            {
                dystrybuanta += prevpopulation.obiektypop[i].probability;
            }

            for (int i = 0; i < il; i++)
            {
                double ods = xdd.NextDouble();
                if (ods < prevpopulation.obiektypop[1].probability)
                {
                    obiektypop.Add(prevpopulation.obiektypop[0]);
                }
                else
                {
                    int el = 0;
                    for (int j = 1; j < il; j++)
                    {
                        if (ods > prevpopulation.obiektypop[j].probability)
                        {
                            el++;
                        }
                    }
                    obiektypop.Add(prevpopulation.obiektypop[el]);
                }
                Console.WriteLine("Obiekt " + obiektypop[i].wynik);
            }


        }
        //Selekcja i zapełnianie populacji: Metoda 2 Rankingowa
        public void selectchildrenm2()
        {
            List<Obiekt> sortedobj = prevpopulation.obiektypop.OrderBy(o => o.probability2).ToList();
            sortedobj.Reverse();
            
            for (int i = 0; i < il; i++)
            {
                int val=xdd.Next(xdd.Next(il));
                //Console.WriteLine(val + " probab "+sortedobj[val].probability2);
                obiektypop.Add(new Obiekt(sortedobj[val], il, nnn, aaa, bbb, mmm));
                // allscores.Add(prevpopulation[0].obiektypop[i].probability);
                //Console.WriteLine(obiektypop[i].wynik+"wasda");
            }
        }
        public void selectchildrenm2min()
        {
            List<Obiekt> sortedobj = prevpopulation.obiektypop.OrderBy(o => o.probability2).ToList();
        

            for (int i = 0; i < il; i++)
            {
                int val = xdd.Next(xdd.Next(il));
               
                obiektypop.Add(sortedobj[val]);
                // allscores.Add(prevpopulation[0].obiektypop[i].probability);
                Console.WriteLine(obiektypop[i].wynik);
            }
        }

        //Selekcja i zapełnianie populacji: Metoda 3 Turniejowa
        public void selectchildrenm3(int xdx)
        {

            int groupcount = 2;
            if (xdx == 0)
            {
                for(int j = 0; j < il; j++)
                {
                    for (int i = 0; i < groupcount; i++)
                    {
                        int randw = xdd.Next(il);
                        tempobiektypop.Add(prevpopulation.obiektypop[randw]);
                    }
                   
                    List<Obiekt> sortedobj2 = tempobiektypop.OrderBy(o => o.probability2).ToList();
                    sortedobj2.Reverse();
                    obiektypop.Add(sortedobj2[0]);
                    sortedobj2.Clear();
                    tempobiektypop.Clear();


                    Console.WriteLine(obiektypop[j].wynik);
                }
            }
            else
            {
                List<int> selectedvals = new List<int>();
                for (int j = 0; j < il; j++)
                {
                    for (int i = 0; i < groupcount; i++)
                    {
                        int randw = xdd.Next(il);
                        while (selectedvals.Contains(randw))
                        {
                            randw = xdd.Next(il);
                        }
                        selectedvals.Add(randw);
                        tempobiektypop.Add(prevpopulation.obiektypop[randw]);
                    }
                    List<Obiekt> sortedobj2 = tempobiektypop.OrderBy(o => o.probability2).ToList();
                    sortedobj2.Reverse();
                    obiektypop.Add(sortedobj2[0]);
                    sortedobj2.Clear();
                    tempobiektypop.Clear();
                    selectedvals.Clear();
                    Console.WriteLine(obiektypop[j].wynik);
                }
            }
            
        }
        public void selectchildrenm3min(int xdx)
        {
            int groupcount = 5;
            if (xdx == 0)
            {
                for (int j = 0; j < il; j++)
                {
                    for (int i = 0; i < groupcount; i++)
                    {
                        int randw = xdd.Next(il);
                        tempobiektypop.Add(prevpopulation.obiektypop[randw]);
                    }

                    List<Obiekt> sortedobj2 = tempobiektypop.OrderBy(o => o.probability2).ToList();
                    
                    obiektypop.Add(sortedobj2[0]);
                    sortedobj2.Clear();
                    tempobiektypop.Clear();
                    Console.WriteLine(obiektypop[j].wynik);
                }
            }
            else
            {
                List<int> selectedvals = new List<int>();
                for (int j = 0; j < il; j++)
                {
                    for (int i = 0; i < groupcount; i++)
                    {
                        int randw = xdd.Next(il);
                        while (selectedvals.Contains(randw))
                        {
                            randw = xdd.Next(il);
                        }
                        selectedvals.Add(randw);
                        tempobiektypop.Add(prevpopulation.obiektypop[randw]);
                    }
                    List<Obiekt> sortedobj2 = tempobiektypop.OrderBy(o => o.probability2).ToList();
                   
                    obiektypop.Add(sortedobj2[0]);
                    sortedobj2.Clear();
                    tempobiektypop.Clear();
                    selectedvals.Clear();
                    Console.WriteLine(obiektypop[j].wynik);
                }
            }
            


        }

        public void mutation()
        {
          
            //Console.WriteLine("wykonuje mutacje");
            //uneditedparents = parentstheme.ToList();
            for(int objpop = 0; objpop < obiektypop.Count; objpop++)
            {
                bool mutated = false;
              //  Console.WriteLine("prze mut" );

                
               
                for (int mutval = 0; mutval < obiektypop[0].longdna.Length; mutval++)
                {
                    if (xdd.NextDouble() < mutationval)
                    {
                        //Console.WriteLine("Zmieniam element "+mutval+" W obiekcie "+objpop);
                        mutated = true;
                        //Console.WriteLine("Zamieniam " + obiektypop[objpop].longdna[mutval]);
                        
                        if(obiektypop[objpop].longdna[mutval] == 1)
                        {
                            obiektypop[objpop].longdna[mutval] = 0;
                            uneditedparents[objpop].longdna[mutval] = 0;
                        }
                        else
                        {
                            obiektypop[objpop].longdna[mutval] = 1;
                            uneditedparents[objpop].longdna[mutval] = 1;
                        }
                        
                        
                        //Console.WriteLine(" NA "+ obiektypop[objpop].longdna[mutval]);
                        //uneditedparents[objpop].longdna[mutval] = 1 - uneditedparents[objpop].longdna[mutval];

                    }
                }
               

                
                obiektypop[objpop].updateshortdna();
                
            }
            for (int afti = 0; afti < il; afti++)
            {
                aftermut.Add(new Obiekt(uneditedparents[afti], il, nnn, aaa, bbb, mmm));
                aftermut[afti].updateshortdna();

            }
            /*
            for (int afti = 0; afti < il; afti++)
            {

                aftermut[afti].printlongdna();

            }
            Console.WriteLine("aftermut count " + aftermut.Count);*/
            //Console.WriteLine("Obiekty populacji "+obiektypop.Count);
            //Console.WriteLine("Obiekty pomutacji " + aftermut.Count);
            //Console.WriteLine("WYPI to w " + aftermut.Count);

            //uneditedparents = parentstheme.ToList();


        }
        public void inversion()
        {
            uneditedparents = parentstheme.ToList();
            for (int objpop = 0; objpop < obiektypop.Count; objpop++)
            {
                bool inversed = false;
                    if (xdd.NextDouble() < invval)
                    {
                    inversed = true;
                        int pp = xdd.Next((obiektypop[0].mm * obiektypop[0].nn)-1);
                        int kp = xdd.Next((obiektypop[0].mm * obiektypop[0].nn-pp)-1)+pp;
                        int tp = kp - pp+1;
                        
                        //Console.WriteLine("Tp:  " + tp + "obiekt:  " +objpop+" zakres od "+pp+" do "+kp);
                        //obiektypop[objpop].printlongdna();
                        for (int ival=0;ival<tp/2;ival++)
                        {
                        //inversion after mutation
                            int temp;
                            temp = obiektypop[objpop].longdna[kp-ival];
                            obiektypop[objpop].longdna[kp - ival] = obiektypop[objpop].longdna[pp + ival];
                           // Console.WriteLine("Zamieniam: " + temp + " na " + obiektypop[objpop].longdna[pp + ival]);
                            obiektypop[objpop].longdna[pp + ival] = temp;

                        //inversion of default list
                            int temp2;
                            temp2 = uneditedparents[objpop].longdna[kp - ival];
                            uneditedparents[objpop].longdna[kp - ival] = uneditedparents[objpop].longdna[pp + ival];
                            //Console.WriteLine("Zamieniam: " + temp2 + " na " + obiektypop[objpop].longdna[pp + ival]);
                            uneditedparents[objpop].longdna[pp + ival] = temp2;
                    }
                        //obiektypop[objpop].printlongdna();
                    }
                obiektypop[objpop].updateshortdna();
                if (inversed)
                {
                    afterinv.Add(new Obiekt(uneditedparents[objpop], il, nnn, aaa, bbb, mmm));
                    afterinv[afterinv.Count-1].updateshortdna();
                    //afterinv[afterinv.Count - 1].printlongdna();
                }

            }
            //Console.WriteLine("PO inwersji ZOSTALO" + afterinv.Count);
            //Console.WriteLine("Po inversji liczba:" +afterinv.Count);

            uneditedparents = parentstheme.ToList();
        }
        int na;

        public Populacja(List<Populacja> prev, int ilo, int n, int a, int b, int m)
        {
            wynikipopulacji = 0;
            nnn = n;
            aaa = a;
            bbb = b;
            mmm = m;
            il = ilo;
            prevpopulation = prev[0];
            calculateprobability();
            for(int wynkal = 0; wynkal < il; wynkal++)
            {
                wynikipopulacji += prevpopulation.obiektypop[wynkal].wynik;
            }
            wynikipopulacji = wynikipopulacji / il;
            if (il > 10)
            {
                wynikipopulacji = wynikipopulacji / (il / 10);
            }
            Console.WriteLine("SREDNI WYNIK POPULACJI " + wynikipopulacji);
            selectchildrenm2();
            //selectchildrenm2min();
            //selectchildrenm1();
            //selectchildrenm1min();
            //selectchildrenm3(1);
            //selectchildrenm3(0);
            //selectchildrenm3min(1);
            //selectchildrenm3min(0);
            //selectchildrenm1();
            for (int vz = 0; vz < obiektypop.Count; vz++)
            {
                obiektypop[vz].transformdna();
            }
            //obiektypop[0].printshortdna();
            for(int wsobjo=0; wsobjo < il; wsobjo++)
            {
                uneditedparents.Add(new Obiekt(obiektypop[wsobjo], il, nnn, aaa, bbb, mmm));
                parentstheme.Add(new Obiekt(obiektypop[wsobjo], il, nnn, aaa, bbb, mmm));

            }
            mutation();
            inversion();
            //crossingpoints(1);

            //evencrossing();
           
            //randomselection();
            //partialselection();

            //wholeselection();
            for (int vz = 0; vz < obiektypop.Count; vz++)
            {
                obiektypop[vz].printlongdna();
            }

            

        }
        public Populacja(int ilo,int n,int a,int b,int m)
        {
            na = n;
            
            for(int i = 0; i < ilo; i++)
            {
                obiektypop.Add(new Obiekt(xd,ilo,n,a,b,m));
            }
            for (int vz = 0; vz < obiektypop.Count; vz++)
            {
                obiektypop[vz].printlongdna();
            }
        }
        public void countval()
        {
            for (int f = 0; f < obiektypop.Count; f++)
            {
                obiektypop[f].count();   
            }
        }
    }
    class Program
    {
        public static readonly Random random = new Random();
        static void Main(string[] args)
        {
            int populacja = 0;
            int lpopulacji = 100;
            int ilo = 10;
            
            int[] dziedzina = new int[] {-1,1};
            List<Populacja> populations = new List<Populacja>();
            List<Populacja> populationspass = new List<Populacja>();
            int dz = dziedzina[1] - dziedzina[0];
            int d = 6; 
            double ik = dz * Math.Pow(10, 6);
            int m = 0;
            int n = 6;
            double dlugosc = 0;
            while (dlugosc < 2 * Math.Pow(10, d))
            {
                m += 1;
                dlugosc = Math.Pow(2, m);
            }
            //Populacje XDD
          
            populations.Add(new Populacja(ilo, n, dziedzina[0], dziedzina[1], m));
            while (populacja < lpopulacji)
            {
                populations[populations.Count - 1].countval();
                Console.WriteLine("Populacja:" + populations.Count());
               
                populationspass.Clear();
                populationspass.Add(populations[populations.Count - 1]);
                //Console.WriteLine("POPULACJA: XDDD "+populationspass.Count);
                populations.Add(new Populacja(populationspass, ilo, n, dziedzina[0], dziedzina[1], m));
                populacja++;
            }
            
            Console.ReadKey();
        }
    }
}
