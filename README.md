<h1>Gezgin Satıcı Problemi(Traver Salesman Problem)</h1>
<p align="justify">
TSP (Travelling Salesman Problem), bir kişinin veya bir elemanın belirlenen şehirler doğrultusunda her bir şehri en az bir kez ziyaret ederek başladığı şehre geri dönmesi için oluşturulan bir rota problemidir. Bu ödevin amacı ise bu probleme dayanarak bu kişinin hesaplanan en kısa rotaya göre(tüm şehirleri dolaşması şartı ile) gidip, en az maliyetle başladığı şehire geri dönmesidir. Bu sayede bu kişi zamandan, bütçeden, yakıttan vs. tasarruf etmiş olacaktır.
Bu uygulamada Gezgin satıcı problemi(Traver Salesman Problem)'nin bruteforce yaklaşımı ile çözümü ele alınmıştır.
Bruteforce yaklaşımı düşük ölçekli dosyalar için global optimum çözüme yakınsar ancak yüksek boyutlu
dosyalar için ise malesef bu işlem hem memory hem zaman açısından çok maliyetli olur, bunun için ise
örneğin, greedy algoritma gerçeklenebilir, greedy algoritma hem memory hem zaman açısından oldukça
verimli olur makul sürede çözümü gerçekleştirir ancak bunun da dezavantajı global aramada başarısız
olması ve local çözümler üretmesidir.</p>

<h3>1. Geliştirme Ortamı ve Kullanılan Dil</h3>
Visual Studio (Console Application(.Net)) - C#


Kullanılan girdi dosyalarını bin->debug->.net7.0 klasörünün içerisinde bulabilirsiniz.(tsp_5_1.txt,tsp_124_1.txt ...)
