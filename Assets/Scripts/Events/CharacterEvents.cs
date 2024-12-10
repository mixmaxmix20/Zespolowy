using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    /*
    Statyczne zdarzenie (akcja) wywoływane, gdy postać otrzymuje obrażenia/zostaje wyleczona.
    Parametry: 
        - GameObject: referencja do obiektu postaci, która otrzymała obrażenia/badź została wyleczona
        - int: ilość obrażeń/przywróconego zdrowia 
    */
    public static UnityAction<GameObject, int> characterDamaged;
    public static UnityAction<GameObject, int> characterHealed;
}
