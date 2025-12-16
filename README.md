# Lab 6: Generics and Collections

## Description
This update transitions data storage to Generic Collections and enforces a separation of concerns by decoupling business logic from the user interface.

## Objectives
* Implement `List<T>` for dynamic object storage.
* Refactor methods to remove direct Console I/O dependencies.
* Expand Unit Tests to cover collection management logic.

## Key Changes
* **Storage:** Replaced fixed arrays with `List<Planet>` for dynamic memory management.
* **Architecture:** Logic methods are now independent of `Console.ReadLine/WriteLine`, making them pure and testable.
* **Collection Operations:**
    * Adding, deleting, and searching are implemented using standard `List<T>` methods.
* **Testing:** Added new unit tests to verify the logic of the collection manager.
