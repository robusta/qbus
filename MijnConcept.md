Allen,

Aangezien ik al een beetje begonnen ben en al heel veel nagedacht heb over hetgeen ik ermee wil bereiken ben ik zo vrij om mijn concept en idee eens uit de doeken te doen!

# De architectuur #

Ik heb thuis dus een installatie met een ETH02 die in mijn thuisnetwerk hangt. Bedoeling is om een server (windows server 2008) te hebben draaien waarvoor ik volgende functies voorzie:
  * hosten van de database (MySql 5.1)
  * applicatieserver voor het hosten van de WCF services (wat deze doen komt verder)
  * applicatieserver voor het hosten van de wpf applicaties
  * server zal ook de service runnen voor speciale functionaliteiten en logging te maken
  * ik draai hier ook een mailserver voor de mails van de applicatie
  * aan de server wordt ook een gsm gekoppeld voor sms-functionaliteiten (geen investering in sms-module)

Voor de gebruikerskant voorzie ik de volgende functies:
  * een full screen WPF stand alone applicatie die ik zal draaien op de touchscreen (gewone touchscreen monitor (17inch) die aan de server zal gekoppeld worden) en ook zal geïnstalleerd worden op de andere pc's in huis
  * een browser based wpf applicatie (eigenlijk dezelfde applicatie, maar dan anders gedeployed met kleine aanpassingen) om via het web toegang te krijgen
  * een light browser client om op te roepen op pc's waar geen .NET 3.0 beschikbaar is en de vorige browser based WPF applicatie niet kan draaien. Hiermee eerder de standaard functionaliteiten voorzien en niet zo uitgebreid als de andere applicatie (denk dit te ontwikkelen in ASP.net)
  * service om via email en sms bepaalde notificaties te krijgen en bepaalde dingen op afstand te kunnen activeren. Bijvoorbeeld verwarming met sms opstarten indien op weg naar huis, email sturen om statusoverzicht via mail terug te krijgen.
  * Ik zou ook graag een applicatie maken die draait op windows mobile om op die manier via gsm/pda de bediening te kunnen doen. Dit is ten eerste de goedkoopste mogelijkheid voor een kleine bijkomende touchscreen en gsm heb ik toch altijd bij dus vanuit de zetel is alles binnen handbereik...

De bedoeling is eigenlijk om voor alle verschillende functies een WCF service te bouwen, bijvoorbeeld: lijst van alle VWzones met hun profiel en temperatuur, detail van een bepaalde vwzone, wijzigen profiel/temp van een bepaalde vwzone,... Die services kunnen dan uit de verschillende applicaties opgeroepen worden: standalone WPF client, WPF browser client, light browser client, windows mobile applicatie, service voor de logging,... Iedereen die dan nog daarop iets wil bouwen kan deze ook weer gebruiken!

Ik maak in mijn applicatie ook gebruik van verschillende classes en verschillende projecten in de solution voor de onderdelen: bvb alles die te maken heeft met db komt in een apart project,...

Ik stel voor dat de geïnteresseerde ontwikkelaars zich even kort via google introduceren in de wereld van WPF (Windows Presentation Foundation) en WCF (Windows Communication Foundation).

# De functionaliteiten #

Ik zou de verschillende functionaliteiten van de applicatie opdelen in een aantal hoofddelen en daaronder een aantal subdelen! Hieronder een overzicht, met een korte beschrijving van hetgeen ik al bedacht heb. Al deze functionaliteiten voorzie ik in de standalone full screen applicatie en de WPF browser applicatie. De meeste ga ik ook voorzien in de light brower en de mobile applicatie. Een aantal zullen ook beschikbaar gemaakt worden via sms en email.

Applicatie zal een screensaver hebben met klok en datum en zone om waarschuwingen: gemist bezoek, binnengekomen email, dringende todo, afspraken van die dag, weer,... (nog te bekijken hoe precies dit in elkaar gaat zitten)

  * **Home** (alle functies van de Qbus komen hierin)
    * Overzicht (bedoeling is een overzicht van het huis te krijgen en dan te navigeren naar bepaalde zone en daar status van alle aanwezige functies te zien)
    * Functies (overzicht van standaard functies die veel gebruikt worden: bijvoorbeeld button 'gaan slapen' die verwarming boven activeert en beneden uitlegt,...)
    * Verlichting (besturing van verlichting in de verschillende zones)
    * Verwarming (overzicht van de zones en de mogelijkheid om instellingen te bekijken en te wijzigen per zone)
    * Toegangscontrole (camera aan poort/deur bekijken, loggings van bezoeken bekijken)
    * Andere (status alarm,...)
> > Bij al van deze functies ook voorzien om van die dingen de loggings en zo op te roepen. Bijvoorbeeld logging van temperatuur in bepaalde zone, duurtijd verwarmenen zone, duurtijd verschillende profielen, hoe lang lamp gebrand heeft, wanneer er alarm zijn geweest,...

  * **Plannnig**
    * Agenda functie voor afspraken van het gezin en bepaalde personen
    * Todo lijstje
    * Boodschappenlijstje met functie om af te drukken (ik wil dat dan geordend per winkel en in de ongevere volgorde bij het doorwandelen van de winkel... :))) )

  * **Communicatie**
    * msn integratie voor een algemeen msn account
    * skype integratie om te bellen met antwoordapparaat
    * email: gezinsinbox en eventueel integratie van priveboxen
    * sms

  * **Financieel**
    * ik zou dit doen om mijn financieel beheer te doen, maar ben nog niet zeker om dit te integreren.
    * mogelijkheid om recurring facturen en zo in te steken in combinatie met recurring inkomen
    * bevestigen betaling facturen
    * wie weet is er ooit de mogelijkheid om dit te koppelen aan internetbanking
    * zou eventueel ook handig vinden om saldo rekening op één of andere manier van het ebanking te halen... Maar nu gekloot natuurlijk met die stomme id-bakjes...

Naast de Gui voorzie ik nog een aantal functionaliteiten met hieronder een korte beschrijving:
  * sms en email bij alarm
  * service om het weer op te halen en weer te geven in de applicatie
  * service om zonsopgang en zonsondergang te bepalen (om bijvoorbeeld op die manier tuinverlichting te activeren en zo)
  * ...

# De GUI #

Ik heb al een aantal screen, maar ben ze opnieuw aan het bouwen voor meerdere schermresoluties aan te kunnen en ze iets dynamischer te maken... Dus ik ga hier een aantal screenshots posten om het idee eens duidelijk te maken... (volgen later)

Misschien moeten jullie ook jullie functies die je zeker wilt ingebouwd zien

Voor al het bovenstaande sta ik natuurlijk open voor opmerkingen, maar je begrijpt wel dat hetgeen ik begonnen ben niet meer ga fundamenteel meer ga wijzigen... Voor de nieuwe delen sta ik volledig open voor jullie input natuurlijk!

Zoals je ziet zit ik wel vol ideeën, maar zal we allemaal even duren. Dus misschien jullie input en bij herlezen zal ik ook wel nog dingen bedenken.