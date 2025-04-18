# Beschrijving van het systeem
Je bent als een nieuwe werknemer aan het werk gegaan voor PlannerSoft, een bedrijf dat planning
software maakt. Een van hun oudere producten, dat nog steeds verkocht wordt, is een persoonlijke
takenlijst applicatie. Aangezien er nog weleens wijzigingen aangemaakt worden, heeft het bedrijf
besloten om de kwaliteitsbewaking voor het product te verbeteren, omdat er helemaal geen
automatische testen ervoor zijn.

Jouw eerste opdracht is om de solution van unittesten te voorzien. Na het door lezen van de
documentatie en een lang gesprek met de product owner en de ontwikkelaars van de frontend,
heb je de volgende acceptatie criteria gekregen.

## Acceptatie criteria
Er zijn een aantal functionaliteiten in dit systeem. De onderstaande criteria gelden voor
alle functies.

1) Als er een onverwachte fout optreedt dan wordt er een Internal Server Error naar de frontend gestuurd.

2) Een client mag niet langer dan 100 milliseconden op een antwoord wachten.

3) Er is een code coverage van 80%.

### Aanmaken van een nieuwe taak
1) Bij het aanmaken van een nieuwe taak wordt er een uniek ID voor de taak gegenereert en opgeslagen.

2) De unieke ID voor de nieuw aangemaakte taak wordt teruggegeven aan de frontend.

3) Een taak wordt standaard aangemaakt met de "nieuw" status.

4) De prioriteit van een nieuwe aangemaakte taak is standaard "normaal".

5) Een taak wordt standaard aangemaakt met een lege beschrijving.

6) De meegegeven samenvatting door de frontend wordt bij de nieuwe taak opgeslagen.

7) Als er een einddatum meegegeven wordt bij het aanmaken van een taak, dan wordt de einddatum opgeslagen.

8) Een taak kan aangemaakt worden zonder einddatum.

9) Als er een begindatum meegegeven wordt bij het aanmaken van een taak, dan wordt de begindatum opgeslagen.

10) Een taak kan aangemaakt worden zonder begindatum.

11) Er wordt een validatie fout aan de frontend teruggegeven als een taak aangemaakt wordt zonder een samenvatting.

12) Er wordt een validatie fout aan de frontend teruggegeven als de einddatum in het verleden ligt.

13) Er wordt een validatie fout aan de frontend teruggegeven als de startdatum in het verleden ligt.

14) Er wordt een validatie fout aan de frontend teruggegeven als de startdatum na de einddatum is.

15) Als er meerdere validaties fout gaan dan worden er ook meerdere validatiefouten teruggegeven.


### Verwijderen van een taak
1) De taak die bij de gegeven unieke ID hoort wordt verwijderd uit de opgeslagen taken.

2) Een succes resultaat wordt aan de frontend terug gegeven als de taak is verwijderd.

3) Het wordt aan de frontend gemeld als een taak die verwijderd moet worden niet bestaat met een Not Found code.

4) Er wordt een validatie fout aan de frontend teruggegeven als een taak wordt verwijderd met een ongeldige ID. 


### Opvragen van een overzicht met taken
1) Als er geen taken zijn dan wordt een lege lijst met taken teruggegeven aan de frontend.

2) Als er taken zijn dan worden alle taken teruggestuurd naar de frontend.

3) Voor iedere taak, die teruggegeven wordt, wordt de opgeslagen unieke ID meegegeven.

4) Voor iedere taak, die teruggegeven wordt, wordt de opgeslagen samenvatting meegegeven.

5) Voor iedere taak, die teruggegeven wordt en een einddatum heeft, wordt de opgeslagen einddatum meegegeven.

6) Heeft een taak geen einddatum, dan wordt er ook niets mee terug gegeven.

7) Voor iedere taak wordt de opgeslagen status van de taak meegegevn.

8) Voor iedere taak wordt de opgeslagen prioriteir van de taak meegeven.


### Het opvragen van de details van een taak.
1) De taak die bij de unieke ID hoort wordt teruggegeven aan de frontend.

2) De frontend krijgt een niet gevonden resultaat als de taak voor de unieke ID niet gevonden kan worden.

3) Er wordt een validatie fout aan de frontend teruggegeven als een taak wordt opgevraagd met een ongeldige ID.

4) De opgeslagen samenvatting wordt teruggegeven met de informatie van de taak.

5) De opgeslagen einddatum wordt teruggegeven met de informatie van de taak.

6) Als er geen einddatum is dan wordt er ook geen einddatum met de taak teruggegeven.

7) De opgeslagen startdatum wordt teruggegeven met de informatie van de taak.

8) Als er geen startdatum is dan wordt er ook geen startdatum met de taak teruggegeven.

9) De opgeslagen status wordt teruggegeven met de informatie van de taak.

10) De opgeslagen prioriteit wordt teruggegeven met de informatie van de taak.

11) De opgeslagen beschrijving wordt teruggegeven met de informatie van de taak.


### Het wijzigen van de details van een taak.
1) De frontend krijgt een not found resultaat als de taak niet bestaat.

2) De frontend krijgt een succes bericht ook als er geen veld een andere waarde krijgt.

3) Als de samenvatting een nieuwe geldige waarde krijgt dan wordt deze opgeslagen.

4) Als de einddatum een nieuwe geldige waarde krijgt dan wordt deze opgeslagen.

5) Als de einddatum leeggelaten wordt dan wordt de einddatum van de opgeslagen taak verwijdert.

6) Als de startdatum een nieuwe geldige waarde krijgt dan wordt deze opgeslagen.

7) Als de startdatum leeggelaten wordt dan wordt de startdatum van de opgeslagen taak verwijdert.

8) Als de status een nieuwe waarde krijgt dan wordt deze opgeslagen.

9) Als de prioriteit een nieuwe waarde krijgt dan wordt deze opgeslagen.

10) Als de beschrijving een nieuwe geldige waarde krijgt dan wordt deze opgeslagen.

11) Er wordt een validatie fout aan de frontend teruggegeven als een taak wordt gewijzigd met een ongeldige ID.

12) Alleen de velden die ook daadwerkelijk gewijzigd worden, moeten gevalideerd worden op een geldige waarde.

13) De frontend krijgt een validatie fout als er een ontbrekende of lege samenvatting meegegeven wordt.

14) De frontend krijgt een validatie fout als de einddatum in het verleden ligt.

15) De frontend krijgt een validatie fout als de startdatum na de einddatum is.

16) De frontend krijgt een validatie fout als er beschrijving een ontbrekende waarde heeft. Een lege beschrijving is wel toegestaan.
