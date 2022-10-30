namespace tutorial

module using_variables_to_store_values=

    printfn "# using_variables_to_store_values"
    
    // Variablen, Binden eines Werts
    (*
        Variablen sind benannte Verweise, die an einen Wert gebunden sind, auf den Sie im Code verweisen können. In F# wird dies als Binden eines Werts bezeichnet. Hierzu weisen Sie einem benannten Verweis (einer Variable) einen Wert zu, d. h. Sie binden diesen Wert. Verwenden Sie zum Binden eines Werts das Schlüsselwort let und einen Namen für Ihren Verweis. Weisen Sie ihm, wie im folgenden Code gezeigt, einen Wert zu:
    *)

    let name1 = "Chris"

    (*
        Hierbei stellt name den benannten Verweis und „Chris“ den gebundenen Wert dar.

        Sobald einer Variable ein Wert zugewiesen wurde, kann dieser nicht mehr geändert werden. Der folgende Code ließe sich nicht kompilieren und würde einen Fehler wie „Fehler FS0027: Dieser Wert ist nicht veränderbar“ erzeugen.
    *)
    let name2 = "Chris"

    // name <- "Luis" // not allowed

    (*
        Veränderlich Machen von Variablen
        Sie können Variablen ändern, aber Sie müssen signalisieren, dass Sie sie veränderlich machen möchten. Mithilfe des Schlüsselworts mutable in der Variablendefinition können Sie einen Wert ändern, ohne einen Kompilierungsfehler hervorzurufen, wie im folgenden Code:
    *)

    let mutable name3 = "Chris"
    name3 <- "Luis" // this statement is now allowed

    // Variablentypen
    (*
        In F# gibt es eine Reihe verschiedener Typen. Viele der Typen haben mit dem Speichern von Zahlen verschiedener Größen und mit oder ohne Dezimalstellen zu tun. Andere Typen sind Textzeichenfolgen- oder booleschen Variablen zugeordnet. Hier sehen Sie eine Liste der Typen, auf die Sie wahrscheinlich stoßen, wenn Sie F# erlernen.

        type	Beschreibung
        bool	Die möglichen Werte sind true oder false.
        INT	Werte von -2.147.483.648 bis 2.147.483.647.
        Zeichenfolge	Unicode-Text
        float, double	Ein 64-Bit-Gleitkommawert.

        Der Typ wird abgeleitet

        Die Deklaration einer Variable kann mit oder ohne Angabe des Typs erfolgen. Wenn Sie beim Deklarieren einer Variable keinen Typ angeben, errät der F#-Compiler den gewünschten Typ anhand des Werts, den Sie der Variable zuweisen. Betrachten Sie folgende Anweisungen:
    *)
    let age = 65 // int
    let PI = 3.14 // float
    let name4 = "my name" // string

    (*
        Der Compiler leitet ab, welche Typen verwendet werden sollen, und liegt damit richtig. Sie können den Typ jedoch auch explizit angeben. Zum Zuweisen von Daten verwenden Sie die Syntax variableName:<type>, wie im folgenden Code:
    *)

    let sum1:float = 0.0

    // Ausgeben auf dem Bildschirm
    (*
        Häufig benötigen Sie die Möglichkeit, Ausgaben auf dem Bildschirm zu erstellen. Dies kann verschiedene Gründe haben, z. B.:

        Anwendungsausgabe: Die Anwendung führt eine Berechnung aus, und Sie möchten die Ausgabe anzeigen.
        Debuggen: Im Rahmen des Debuggens ihres Codes müssen Sie möglicherweise das Ergebnis an einem bestimmten Punkt ausgeben, um zu verstehen, was nicht nach Plan läuft.
        Es gibt andere Gründe für den Wunsch nach einer Bildschirmausgabe, aber die beiden eben genannten Szenarien sind die gängigsten.

        Wie möchten Sie also die Bildschirmausgabe erstellen? In F# gibt es drei verschiedene Funktionen, die Sie verwenden können. Dies sind printf, printfn und sogar Console.WriteLine. Wo liegt nun der Unterschied?

        printf: Die Druckausgabe erfolgt auf stdout inline (ohne Zeilenvorschubzeichen).
        printfn : Die Druckausgabe erfolgt auf stdout, und es wird ein Zeilenvorschubzeichen hinzugefügt.
        Console.WriteLine: Diese Funktion stammt aus dem System-Namespace und funktioniert in allen .NET-Sprachen.
        Nun kennen Sie den Unterschied – aber welche sollten Sie verwenden? Naja, printf und printfn werden als idiomatischer angesehen und werden daher in F# bevorzugt.   
    *)

    // Formatierung
    (*
        Im Rahmen der Bildschirmausgabe möchten Sie möglicherweise Text und Zahlen kombinieren. Oder Sie wünschen sich eine in einer bestimmten Weise formatierte Ausgabe, etwa mit diesen Mitteln:

        Positionsargumente: Zum Formatieren können Sie eine .NET-Funktion wie string.Format verwenden, die ihrerseits von Positionsargumenten wie string.Format("My name is {0} and I live in {1}", "Chris", "UK") Gebrauch macht.

        Zeichenfolgeninterpolation: Eine weitere Möglichkeit zum Kombinieren von Variablen und Text ist die Verwendung einer sogenannten Interpolation. Um sie zu verwenden, müssen Sie der Zeichenfolge ein $-Zeichen voranstellen und Platzhalter mit Klammern {} angeben. Hier sehen Sie ein Beispiel für die Verwendung von Interpolation:
    *)

    let name5 = "Luis"
    let company5 = "Microsoft"
    printfn $"Name: {name5}, Company: {company5}"

    (*
        Zwischen den Klammern können Sie auch Ausdrücke hinzufügen, wie hier zu sehen:
    *)

    let firstNumber = 2000
    let secondNumber = 21
    printfn $"The year is: {firstNumber + secondNumber}"

    (*
        Hinweis

        Bei der Verwendung von Interpolation gibt es keine Typprüfung, daher scheint sie einfach in der Anwendung zu sein. Achten Sie darauf, alle Elemente richtig zu kombinieren.

        Spezifizierer: Sie können auch Formatbezeichner als Teil des auszugebenden Ausdrucks verwenden. Die Verwendung von Spezifizierern ist die am häufigsten verwendete Methode zum Formatieren in F#. Hier sehen Sie ein Beispiel:
    *)

    let name6 = "Chris"
    printfn "Hi %s" name6
    // prints: Hi Chris

    (*
        Hier sehen Sie, wie das Formatierungszeichen %s verwendet wird, um die erste Zeichenfolge mit der Variable name zu mischen.

        Hinweis

        Wenn Sie Formatierer wie %s oder %i verwenden, führt der Compiler eine Typprüfung durch. Wenn Ihr Positionsargument nicht dem von Ihnen angegebenen Typ entspricht, löst er einen Fehler aus.

        Formatbezeichner
        Es gibt viele Formatbezeichner. Hier finden Sie einige, auf die Sie wahrscheinlich stoßen werden.

        Bezeichner	Beschreibung
        %3!	        Wird für Zeichenfolgen und Inhalte ohne Escapesequenz verwendet.
                    printf "Hello %s" name
        %d, %i	    Als ganze Dezimalzahl formatiert, mit Vorzeichen, wenn der zugrunde liegende Ganzzahltyp mit Vorzeichen angegeben ist	
                    printf "Age: %i" 65
        %b	        Boolesches true oder false
                    printf "Setting on: %b" true

        Es gibt viele weitere Informationen zur Formatierung. Informationen zu allen Funktionen finden Sie in der Dokumentation unter Formatierung in F#.
    *)

    printfn "Hi %s3!" "format %3!"

    // EXERCISE

    printfn "Welcome to the calculator program"
    // read input from the console and assign to `sum`
    let sum = 0
    printfn "The sum is %i" sum