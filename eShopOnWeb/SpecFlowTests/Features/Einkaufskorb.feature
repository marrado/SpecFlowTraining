Funktionalität: Waren in Einkaufswagen hinzufügen

Grundlage: 
	Gegeben sei eine leere in-Memory Datenbank

#-------------------------------------------------------------------------------------------------------------------------------------------

Szenario: Ein Produkt in leeren Einkaufswagen hinzufügen

	Angenommen mein Einkaufswagen ist leer
	
	Wenn ich einen Produkt in Einkaufswagen hinzufüge

	Dann sollte mein Einkaufswagen 1 Produkt beinhalten

#-------------------------------------------------------------------------------------------------------------------------------------------

Szenario: Ein Produkt in nicht leeren Einkaufswagen hinzufügen

	Angenommen mein Einkaufswagen hat schon 2 Produkte
	
	Wenn ich einen Produkt in Einkaufswagen hinzufüge

	Dann sollte mein Einkaufswagen 3 Produkte beinhalten

#-------------------------------------------------------------------------------------------------------------------------------------------

Szenariogrundriss: Mehrere Produkte in leeren Einkaufswagen hinzufügen

	Angenommen mein Einkaufswagen ist leer
	
	Wenn ich <Anzahl> Produkte in Einkaufswagen hinzufüge

	Dann sollte mein Einkaufswagen <Anzahl> Produkte beinhalten

	Beispiele:
	| Anzahl |
	| 2      |
	| 4      |
	| 5      |