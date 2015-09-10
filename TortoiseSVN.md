# Introductie #

Om onze code te delen met andere gebruikers, gaan we gebruik maken van een versiebeheersysteem: SubVersion. Google Code maakt hiervan gebruik. Dit systeem zorgt ervoor dat er nooit wijzigingen verloren kunnen gaan of wijzigingen in broncode overschreven kunnen worden door iemand.


# Details #

Om TortoiseSVN te installeren download je de laatste versie van de site:
http://tortoisesvn.tigris.org/

# Gebruik #

1. Installeer de TortoiseSVN client en herstart de PC

2. Maak een map ergens op je schijf aan

3. Rechtsklik op deze map en kies voor "SVN Checkout..."

4. Vul als repository url https://qbus.googlecode.com/svn/trunk/ in en druk op OK

5. Bij het vragen naar een login + wachtwoord vul je je Google account in (vb. goens.roel) en het repository wachtwoord, NIET je google account wachtwoord. Het repository wachtwoord kun je vinden door op Settings te klikken op de project portal (rechtsboven).

6. TortoiseSVN haalt nu de bestanden van de server en zet deze lokaal in de map

Hierna kun je de files gaan bewerken naar believen en je wijzigingen terug op de server plaatsen. Dit door terug rechts te klikken op de map en "SVN Commit..." te kiezen. De client zal detecteren welke files gewijzigd zijn en deze voorstellen. Bovenaan kan je commentaar schrijven over wat je gewijzigd hebt en daarna kun je alles committen. Bij conflicten (mensen die voor jou reeds wijzigingen hebben weggeschreven), zul je manueel je wijzigingen moeten gaan "mergen" met die van de vorige gebruiker.

Om je lokale folder te updaten met de laatste wijzigingen op de server kies je voor het command "SVN Update..."

**Zorg er voor dat je bij het comitten altijd een omschrijving meegeeft!**