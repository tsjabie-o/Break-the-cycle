# Break the cycle

Dit programma neemt een gerichte graaf als input. Als er voor een graaf een topologische ordening bestaat, geeft het programma deze als output. In het geval dat er cykels in de graaf zitten, wordt gekeken of de cykel kan worden verholpen door het verwijderen van 1 kant. Is dit het geval, dan is de output de verwijderde kant, en de topologische volgorde. Is dit niet het geval, geeft het programma aan dat er "no fix" is. Dit gebeurt voor alle gegeven grafen.

Op de eerste regel van de invoer staat een positief geheel getal, het aantal grafen in de testcase. Daarna volgt per graaf:
- een positief getal n, het aantal kanten in de graaf (kanten worden gerepresenteert als cijfers startend van 0)
- een positief getal m, het aantal kanten in de graaf
- m regels met op elk daarvan twee getallen x en y. Dat representeert een kant van x naar y.

Eerst wordt voor elke graaf een Graaf-object geinitialiseerd. Die gebruikt de informatie uit de input
om een dictionary representatie van de graaf te maken, nodes als keys, bijbehorende kanten als values. Alle informatie kan nu makkelijk per Graaf-object gevonden worden. Dit maakt het programma een stuk overzichtelijker.

Dan wordt met een Graaf-object als parameter, een OrderedNodes-object aangemaakt. In deze class wordt ook al het sorteer en cykel detecteer werk gedaan, om het programma overzichtelijk te houden.
De class method GetOutput geeft de nodige output, zoals bijvoorbeeld een topologische ordening of "no fix". Zo kan de Main makkelijk van alle grafen de outputs verzamelen en in een keer als output van het programma geven. 

Voor het vinden van de topologische ordening wordt een depth-first algoritme gebruikt. Deze is zelf geimplementeerd voor deze opdracht, om gebruik te maken van mijn eigen classes, maar verschilt in feite niet van een standaard DFS algoritme.
Het algoritme gebruikt een stack om de gezochte nodes bij te houden. Als een gevonden node A al op de stack staat, heeft het algoritme A al bezocht, maar is wel via A bij A terechtgekomen. Dan is er dus een cykel ontdekt. Voor het verhelpen van de cykel wordt door alle kanten van het Graaf-object gekeken en voor alle kanten geprobeerd of een cykel verholpen is door de kant te verwijderen.