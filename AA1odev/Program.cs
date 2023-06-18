using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.ImageSharp;
using OxyPlot.Series;
using SkiaSharp;

namespace TSP;

internal class Program
{
    private static void Main(string[] args)
    {
        //Süre başlatıldı.
        var time = new System.Diagnostics.Stopwatch();
        time.Start();
        // Dosya adı ve koordinat listesi.
        string fileName = "tsp_5_1.txt";
        List<double[]> coordList = new List<double[]>();

        // Dosyada verilen şehirlerdeki koordinatları oku.
        using (StreamReader streamReader = new StreamReader(fileName))
        {
            string line;

            while ((line = streamReader.ReadLine()) != null)
            {
                string[] splitCoords = line.Split(' ');
                double x = double.Parse(splitCoords[0]);
                double y = double.Parse(splitCoords[1]);
                double[] coord = new double[] { x, y };
                coordList.Add(coord);
            }
        }

        // Nearest Neighbor algoritması uygulanarak, en yakın ziyaret edilmemiş tüm noktaları dolaşır ve en kısa mesafeyi hesaplar.
        List<int> shortTour = NearestNeighborAlgorithm(coordList); //Nearest Neighbor algoritması uygulandı.

        // En kısa tur:
        Console.WriteLine("En kısa tur:");
        foreach (int i in shortTour)
        {
            Console.Write("{0} ", i);
        }
        Console.WriteLine();
        time.Stop();
        Console.WriteLine("Çalışma süresi: " + time.ElapsedMilliseconds + "ms");
    }
    static List<int> NearestNeighborAlgorithm(List<double[]> coords) // NearestNeighbor Algoritmasının uygulandığı metot, parametre olarak dosyadaki tüm şehirlerin kooordinatlarını alır.
    {
        // Başlangıç şehrini belirle
        int beginCity = 0;

        // Şehirleri ziyaret listesini oluştur
        List<int> tour = new List<int>();
        tour.Add(beginCity);

        // Ziyaret edilecek şehirleri listesinden kaldır
        List<int> unVisitedCities = new List<int>(); //ziyaret edilmemiş şehirlerin listesi.
        for (int i = 0; i < coords.Count; i++) //tüm şehirleri dolaşır ve ziyaret edilmemiş şehirlerin tutulduğu listeye ekler.
        {
            if (i != beginCity)
            {
                unVisitedCities.Add(i);
            }
        }

        // En yakın şehri bul ve ziyaret sırası listesine ekle, En kısa turu bul.
        double totalDistance = 0; //en kısa turun toplam mesafesini tutan değişken.
        while (unVisitedCities.Count > 0) //tüm şehirler ziyaret edilene kadar döngü devam eder.
        {
            int currentCity = tour[tour.Count - 1]; //mevcut şehir,algoritmada son ziyaret edilen şehir.
            int nearestCityToCurrentCity = -1; //CurrentCity'e en kısa mesafede olan şehir.
            double nearestDistanceToCurrentCity = double.MaxValue; //En yakın şehrin mesafesinin tutulduğu değişken.
            foreach (int city in unVisitedCities) //Distance adındaki metot çağrılarak ziyaret edilmemiş tüm şehirler arasında currentCity'e en yakın şehir belirlenir.
            {
                double distanceBetweenCurrentCityWithUnVisitedCity = Distance(coords[currentCity], coords[city]); //Mevcut(şuanki) şehir ile ziyaret edilmemiş şehir arasındaki mesafe hesaplandı.
                if (distanceBetweenCurrentCityWithUnVisitedCity < nearestDistanceToCurrentCity) //eğer ziyaret edilmemiş şehir'in mevcut şehir ile olan mesafesi, en kısa mesafe olarak belirlenen NearestDistanceToCurrentCity mesafesinden daha kısa ise, o zaman bu şehir NearestCity ve bu mesafe ise NearestDistance olarak güncelleme yapılır.
                {
                    //bahsedilen güncelleme işlemi gerçekleştirildi.
                    nearestCityToCurrentCity = city;
                    nearestDistanceToCurrentCity = distanceBetweenCurrentCityWithUnVisitedCity;
                }
            }
            tour.Add(nearestCityToCurrentCity); //currentCity'e en kısa mesafesi olan NearestCity en kısa tura eklendi.
            unVisitedCities.Remove(nearestCityToCurrentCity); //ve bu şehir, ziyaret edilmemiş şehirlerin listesinden çıkarıldı.
            totalDistance += nearestDistanceToCurrentCity; //NearestDistance(en kısa mesafe) değeri toplam en kısa mesafe değişkenine eklendi.
        }
        totalDistance += Distance(coords[tour[tour.Count - 1]], coords[beginCity]);//Son şehirden başlangıç şehrine olan mesafe hesaplandı ve toplam mesafeye eklendi ve bu sayede tur tamamlanmış ve başlangıç şehrine dönülmüş oluyor.
        tour.Add(beginCity); //başlanılan şehire dönüldü.

        Console.WriteLine("Toplam mesafe: " + totalDistance); //toplam en kısa mesafe ekrana yazıldı.


        return tour; //en kısa tur geriye dönüldü.
    }
    static double Distance(double[] coord1, double[] coord2) //İki şehir arasındaki mesafenin öklid formülüne göre hesaplandığı fonksiyon. Parametre olarak 2 şehirin x ve y koordinatlarını alır.
    {
        double differenceX = coord1[0] - coord2[0]; //İki şehirin x koordinatları arasındaki mesafeyi hesaplar.
        double differenceY = coord1[1] - coord2[1];//İki şehirin y koordinatları arasındaki mesafeyi hesaplar.
        return Math.Sqrt(differenceX * differenceX + differenceY * differenceY); //Öklid formülüne göre (|coord1coord2| = √(coord1[0] - coord2[0]^2 + (coord1[1] - coord2[1])^2)) iki şehir arası uzaklık hesaplandı ve geriye döndürüldü.
    }
}