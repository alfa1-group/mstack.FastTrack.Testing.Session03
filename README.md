# Introductie
Het doel van deze opgave is dat jullie leren om de acceptatie criteria van een legacy product
om te zetten in unittesten. Daarnaast kunnen jullie ook ervaring op doen met testtools waar
je nog niet bekend mee bent.


# Opdrachten
In deze solution vindt je een bestaande applicatie die mogelijk werkt, want het is al verkocht
en niemand heeft er over geklaagd. Maar dat is niet zeker want er zijn geen automatische testen.
In *Acceptatie criteria.md* vindt je een volledige lijst met de acceptatie criteria waar de
applicatie aan moet voldoen. Deze lijst is opgesteld aan de hand van gesprekken met de business
en de klanten.

## Opdracht 1
In de eerste fase gaan jullie aan de slag om puur unittesten aan te maken. Hierbij kiezen jullie
een test framework uit en een tool om mocks te maken. Je bent waarschijnlijk al gewend om met
één framework te werken. Ten behoeve van kennis uitbreiding en ervaring wordt er van je gevraagd
om een framework te kiezen waar je nog geen ervaring mee hebt.

De volgende frameworks zijn er en hier kan je ook meer informatie vinden:
* __MSTest__: https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-mstest-intro
* __NUnit__: https://docs.nunit.org/index.html
* __xUnit__: https://xunit.net/
* __TUnit__: https://thomhurst.github.io/TUnit/docs/intro/

De volgende opties voor het maken van mocks zijn er:
* __Moq__: https://github.com/devlooped/moq/wiki/Quickstart
* __FakeItEasy__: https://fakeiteasy.github.io/
* __NSubstitute__: https://nsubstitute.github.io/
* Maak je eigen fakes/mocks/stubs

Het is mogelijk dat de code in de huidige staat niet goed te testen is. Het wordt aan jullie
gevraagd om de wijzigingen aan te brengen zodat de code wel te testen is.

Het is niet nodig om tijdens de trainingsessie al alle acceptatie criteria omgezet te hebben
in unittesten. Het afmaken is ook huiswerk.


## Opdracht 2
In deze oefening is het de bedoeling om je unittesten te verrijken met de tools die we net
besproken hebben. Focus hierop de volgende tools:

* __Autofixture__: https://autofixture.github.io/
* __FluentAssertions__: https://fluentassertions.com/introduction
* __ValueObjects__
* __Factory Pattern__
* __Builder Pattern__

Als je nog tijd over hebt dan kan je teruggaan naar de vorige opdracht om nog extra unittesten
te maken die je wel of niet direct verrijkt met de besproken tools.

