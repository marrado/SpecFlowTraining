Funktionalität: Anzeigen der Waren auf Hauptseite

Grundlage: 
	Gegeben sei eine leere in-Memory Datenbank

#-------------------------------------------------------------------------------------------------------------------------------------------

Szenario: Es gibt 8 Waren, alle sollten angezeigt werden
	Mit standarte Einstellungen - 10 Waren pro Seite

	Angenommen es 8 Waren in dem Katalog gibt

	Wenn ich die erste Seite von Waren anfordere

	Dann sollten alle diese Waren angezeigt werden
	
#-------------------------------------------------------------------------------------------------------------------------------------------

Szenariogrundriss: Es gibt nicht mehr als 10 Waren, alle sollen angezeigt werden
	Mit standarte Einstellungen - 10 Waren pro Seite

	Angenommen es <Warenanzahl> Waren in dem Katalog gibt

	Wenn ich die erste Seite von Waren anfordere

	Dann sollten alle diese Waren angezeigt werden

Beispiele: 
	| Warenanzahl |
	| 0           |
	| 8           |
	| 10          |
	
#-------------------------------------------------------------------------------------------------------------------------------------------

Szenariogrundriss: Es gibt mehr als 10 Waren, zusätzliche sollen auf nächsten Seiten landen

	Angenommen es <Warenanzahl> Waren in dem Katalog gibt

		Und es bis 10 Waren auf einer Seite gibt

	Wenn ich Seite <Seite> von Waren anfordere

	Dann sollen <Anzahl aufgelisteter Waren> Waren angezeigt werden

Beispiele: 
	| Warenanzahl | Seite | Anzahl aufgelisteter Waren |
	| 8           | 2     | 0                          |
	| 11          | 2     | 1                          |
	| 15          | 2     | 5                          |
	| 20          | 2     | 10                         |
	| 21          | 2     | 10                         |
	| 21          | 3     | 1                          |

#-------------------------------------------------------------------------------------------------------------------------------------------

Szenariogrundriss: Es gibt mehr als 10 Waren, zusätzliche sollen auf nächsten Seiten landen. Unterschiedliche Seitengroßen

	Angenommen es <Warenanzahl> Waren in dem Katalog gibt

		Und es bis <Waren pro Seite> Waren auf einer Seite gibt

	Wenn ich Seite <Seite> von Waren anfordere

	Dann sollen <Anzahl aufgelisteter Waren> Waren angezeigt werden

Beispiele: 
	| Warenanzahl | Seite | Anzahl aufgelisteter Waren | Waren pro Seite |
	| 8           | 2     | 3                          | 5               |
	| 8           | 2     | 2                          | 2               |
	| 8           | 2     | 0                          | 8               |