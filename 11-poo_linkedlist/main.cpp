#include "link.cpp"
#include "linkedlist.cpp"

int main()
{
	LinkedList *linked_list = 0;

	linked_list = new LinkedList();

	linked_list->add(11);
	linked_list->print();
	linked_list->add(22);
	linked_list->print();
	linked_list->add(33);
	linked_list->print();
	linked_list->add(44);
	linked_list->print();
	linked_list->add(55);
	linked_list->print();
	linked_list->add(66);
	linked_list->print();

	linked_list->remove(33);
	linked_list->print();

	Link *found = linked_list->search(33);

	delete linked_list;

	return 0;
}