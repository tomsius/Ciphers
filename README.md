Šifravimo metodų komponentas yra skirtas žmonėms, norintiems išbandyti įvairius šifravimo būdus bei pamatyti jų saugumo trūkumus. Komponentas turi tokius simetrinius šifravimo metodus, kuriais gali užšifruoti bei iššifruoti:
1.	Caesar.
2.	Vigenere.
3.	Hill.
4.	Playfair.

Taip pat komponentas turi asimetrinį šifravimo metodą – RSA. Šiuo būdu galima ne tik šifruoti ir užšifruoti, bet ir pasirašyti bei patikrinti parašą.

Įgyvendintas funkcionalumas:
1.	Šifravimas Caesar metodu.
2.	Iššifravimas Caesar metodu.
3.	Šifravimas Vigenere metodu.
4.	Iššifravimas Vigenere metodu.
5.	Šifravimas Hill metodu.
6.	Iššifravimas Hill metodu.
7.	Šifravimas Playfair metodu.
8.	Iššifravimas Playfair metodu.
9.	Šifravimas RSA metodu.
10.	Iššifravimas RSA metodu.
11.	Pasirašymas RSA metodu.
12.	Parašo patikrinimas RSA metodu.

Šį komponentą galima naudoti tik mokymosi ar tyrinėjimo tikslais, nes realizuotas RSA šifravimo būdas neužtikrina pakankamai ilgų slaptų raktų, todėl jie gali būti lengvai nulaužiami.
Komponentas yra skirtas .NET Standard 2.1 karkasui, todėl jį galima naudoti tiek .NET Framework, tiek .NET Core. Komponentas nenaudoja kitų trečiųjų šalių komponentų, tik bazinius .NET karkaso komponentus.
