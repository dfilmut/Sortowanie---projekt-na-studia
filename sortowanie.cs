using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Sortowanie
{
    class Program
    {
        static Random rnd = new Random(Guid.NewGuid().GetHashCode());

        static void copyTab()
        {
            for (int i = 0; i < TestTab.Length; i++)
            {
                SecondTab[i] = TestTab[i];
            }
        }

        static void Generate_Random_Int(int TabSize)
        {
            for (int i = 0; i < TabSize; i++)
            {
                TestTab[i] = rnd.Next(); 
            }
        }
        static void Generate_Increasing_Int(int TabSize)
        {
            TestTab[0] = rnd.Next();
            for (int i = 1; i < TabSize; i++)
            {
                TestTab[i] = rnd.Next()+TestTab[i - 1];
            }
        }

        static void Generate_Decreasing_Int(int TabSize)
        {
            TestTab[0] = rnd.Next();
            for (int i = 1; i < TabSize; i++)
            {
                TestTab[i] = TestTab[i - 1] - rnd.Next();
            }
        }

        static void Generate_Const_Int(int TabSize)
        {

            int a = rnd.Next();
            for (int i = 0; i < TabSize; i++)
            {
                TestTab[i] = a;
            }
        }

        static void Generate_Vshape_Int(int TabSize)
        {
            int count = 0;
            
            for (int i = TabSize; i > 0; i--)
            {
                if (i % 2 != 0)
                {
                    TestTab[count++] = i;
                }
            }

            for (int i = 1; i <= TabSize; i++)
            {
                if (i % 2 == 0)
                {
                    TestTab[count++] = i;
                }
            }
        }

        static void Generate_Ashape_Int(int TabSize)
        {
            int count = 0;

            for (int i = 1; i < TabSize; i++)
            {
                if (i % 2 != 0)
                {
                    TestTab[count++] = i;
                }
            }

            for (int i = TabSize; i >0; i--)
            {
                if (i % 2 == 0)
                {
                    TestTab[count++] = i;
                }
            }
        }

        static void InsertionSort(int[] t)
        {
            for (uint i = 1; i < t.Length; i++)
            {
                uint j = i;       // elementy 0 .. i-1 są już posortowane
                int Buf = t[j];   // bierzemy i-ty (j-ty) element
                while ((j > 0) && (t[j - 1] > Buf))
                { // przesuwamy elementy
                    t[j] = t[j - 1];
                    j--;
                }
                t[j] = Buf; // i wpisujemy na docelowe miejsce
            }
        }

        static void SelectionSort(int[] t)
        {
            uint k;
            for (uint i = 0; i < (t.Length - 1); i++)
            {
                int Buf = t[i];   // bierzemy i-ty element
                k = i;       // i jego indeks
                for (uint j = i + 1; j < t.Length; j++)
                    if (t[j] < Buf) // szukamy najmniejszego z prawej
                    {
                        k = j;
                        Buf = t[j];
                    }
                t[k] = t[i]; // zamieniamy i-ty z k-tym
                t[i] = Buf;
            }
        }
        // lewy to jest 0 a prawy jest wyznaczany coś tam pierdolenie
        static void Heapify(int[] t, uint left, uint right) //procedura kopcowania
        { //procedura budowania/naprawiania kopca
            uint i = left,
                 j = 2 * i + 1;
            int buf = t[i];      // ojciec
            while (j <= right)    // przesiewamy do dna stogu
            {
                if (j < right)      // wybieramy większego syna
                    if (t[j] < t[j + 1]) j++;
                if (buf >= t[j]) break;
                t[i] = t[j];
                i = j;
                j = 2 * i + 1;     // przechodzimy do dzieci syna
            }
            t[i] = buf;
        }

        static void HeapSort(int[] t)
        {
            uint left = ((uint)t.Length / 2),
                 right = (uint)t.Length - 1;
            while (left > 0)           // budujemy kopiec idąc od połowy tablicy
            {
                left--;
                Heapify(t, left, right);
            }
            while (right > 0)                // rozbieramy kopiec
            {
                int buf = t[left];
                t[left] = t[right];
                t[right] = buf;                // największy element
                right--;                       // kopiec jest mniejszy
                Heapify(t, left, right);       // ale trzeba go naprawić
            }

        }

        static void CocktailSort(int[] t)
        {
            int Left = 1, Right = t.Length - 1, k = t.Length - 1;
            do
            {
                for (int j = Right; j >= Left; j--) // przesiewanie od dołu
                    if (t[j - 1] > t[j])
                    {
                        int Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                        k = j;                  // zamiana elementów i zapamiętanie indeksu
                    }
                Left = k + 1;                 // zacieśnienie lewej granicy
                for (int j = Left; j <= Right; j++) // przesiewanie od góry
                    if (t[j - 1] > t[j])
                    {
                        int Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                        k = j;                  // zamiana elementów i zapamiętanie indeksu
                    }
                Right = k - 1;               // zacieśnienie prawej granicy
            }
            while (Left <= Right);
        }


        static void qsort_rek(int[] t, int l, int p)
        {
            int i, j, x;
            i = l;
            j = p;
            x = t[(l + p) / 2]; //środkowy co do połozenia - 
            //x = t[t.Length - 1]; //prawy końcowy (ostatni element tablicy) co do połozenia - 
            //x = t[rnd.Next(t.Length - 1)]; //losowa co do polozenia
            do
            {
                while (t[i] < x) i++; //przesuwamy indeksy z lewej
                while (x < t[j]) j--; //przesuwamy indeksy z prawej
                if (i <= j)           //jeśli nie mineliśmy się z indeksami (koniec kroku)
                {                     //zamieniamy elementy
                    int buf = t[i]; t[i] = t[j]; t[j] = buf;
                    i++; j--;
                }
            }
            while (i <= j);
            if (l < j) qsort_rek(t, l, j);// sortujemy lewą część (jeśli jest)
            if (i < p) qsort_rek(t, i, p);// sortujemy prawą część (jeśli jest)
        }

        static void qsort_ite(int[] t)
        {
            int i, j, l, p, sp;
            int[] stos_l = new int[t.Length], //przechowywanie żądań podziału
                  stos_p = new int[t.Length]; //przechowywanie żądań podziału
            sp = 0; stos_l[sp] = 0; stos_p[sp] = t.Length - 1;  // rozpoczynamy od całej tablicy

            do
            {
                l = stos_l[sp]; p = stos_p[sp]; sp--;          // pobieramy żądanie podziału
                do
                {
                    int x;
                    i = l; j = p; x = t[(l + p) / 2]; //analogicznie do wersji rekurencyjnej
                    do
                    {
                        while (t[i] < x) i++;
                        while (x < t[j]) j--;
                        if (i <= j)
                        {
                            int buf = t[i]; t[i] = t[j]; t[j] = buf;
                            i++; j--;
                        }
                    } while (i <= j);
                    if (i < p) { sp++; stos_l[sp] = i; stos_p[sp] = p; } // ewentualnie dodajemy żądanie podziału
                    p = j;
                } while (l < p);
            } while (sp >= 0);
        }

        static void CheckTimeSort()
        {
            copyTab();

            DateTime startTime = DateTime.Now;
            InsertionSort(SecondTab);
            DateTime endTime = DateTime.Now;
            TimeSpan ts = endTime - startTime;
            Console.WriteLine("Czas dla InsertionSort: " + ts.TotalMilliseconds);

            //SelectionSort
            copyTab();
            startTime = DateTime.Now;
            SelectionSort(SecondTab);
            endTime = DateTime.Now;
            ts = endTime - startTime;
            Console.WriteLine("Czas dla SelectionSort: " + ts.TotalMilliseconds);

            //HeapSort
            copyTab();
            startTime = DateTime.Now;
            HeapSort(SecondTab);
            endTime = DateTime.Now;
            ts = endTime - startTime;
            Console.WriteLine("Czas dla HeapSort: " + ts.TotalMilliseconds);

            //CocktailSort
            copyTab();
            startTime = DateTime.Now;
            CocktailSort(SecondTab);
            endTime = DateTime.Now;
            ts = endTime - startTime;
            Console.WriteLine("Czas dla CocktailSort: " + ts.TotalMilliseconds);
        }

        static int[] TestTab; //globalna tablica
        static int[] SecondTab; //globalna tablica też
        static void Main(string[] args)
        {
            //kod odpowiedzialny za utworzenie pustej tablicy, co obrót zwięszka rozmiar tablicy, wypełniając nowymi danymi za każdym obrotem        
            for (int TabSize = 50000; TabSize <= 200000; TabSize += 10000)
            {
                Console.Write("Rozmiar tablicy(TabSize): {0} \n", TabSize);
                TestTab = new int[TabSize];
                SecondTab = new int[TabSize];

                Console.WriteLine("Losowe liczby: ");
                Generate_Random_Int(TabSize);
                CheckTimeSort();

                Console.WriteLine("Rosnące liczby: ");
                Generate_Increasing_Int(TabSize);
                CheckTimeSort();

                Console.WriteLine("Malejące liczby: ");
                Generate_Decreasing_Int(TabSize);
                CheckTimeSort();

                Console.WriteLine("Stale liczby: ");
                Generate_Const_Int(TabSize);
                CheckTimeSort();

                Console.WriteLine("V-ksztaltne liczby: ");
                Generate_Vshape_Int(TabSize);
                CheckTimeSort();
            }

            for (int TabSize = 50000; TabSize <= 200000; TabSize += 10000)
            {
                Console.Write("Rozmiar tablicy(TabSize): {0} \n", TabSize);
                TestTab = new int[TabSize];
                SecondTab = new int[TabSize];

                Generate_Random_Int(TabSize);

                DateTime startTime = DateTime.Now;
                qsort_ite(SecondTab);
                DateTime endTime = DateTime.Now;
                TimeSpan ts = endTime - startTime;
                Console.WriteLine("Czas dla QuickSorta Iteracyjnie: " + ts.TotalMilliseconds);

                Generate_Random_Int(TabSize);

                startTime = DateTime.Now;
                qsort_rek(SecondTab, 0, TabSize - 1);
                endTime = DateTime.Now;
                ts = endTime - startTime;
                Console.WriteLine("Czas dla QuickSorta Rekurencyjnie: " + ts.TotalMilliseconds);

                Generate_Ashape_Int(TabSize);
                startTime = DateTime.Now;
                qsort_ite(SecondTab);
                endTime = DateTime.Now;
                ts = endTime - startTime;
                Console.WriteLine("Czas Quicksorta Iteracyjny dla liczb A-ksztaltnych: " + ts.TotalMilliseconds);

                Generate_Ashape_Int(TabSize);
                startTime = DateTime.Now;
                qsort_rek(SecondTab, 0, TabSize - 1);
                endTime = DateTime.Now;
                ts = endTime - startTime;
                Console.WriteLine("Czas Quicksorta Rekurencyjny dla liczb A-ksztaltnych: " + ts.TotalMilliseconds);

            }

            Console.ReadKey();
        }
    }
}
