Your method is generic for <T> relies on to access 4 properties: 'type', 'id', 'name' and 'city'. So it's not generic for "any T" rather it's generic for "any T where 'type', 'id', 'name' and 'city' exist".

This is well and good, however it requires constraining your generic method by defining some interface, for example:

    interface IMyConstraint
    {
        string type { get; }
        int id { get; }
        string name { get; }
        string city { get; }
    }

Now your generic method can confidently access those



