#ifndef LINKEDLIST_H
#define LINKEDLIST_H


#include "link.h"

class LinkedList
{
private:
	Link* m_first;
	Link* m_last;
	int   m_size;

public:
	LinkedList();
	~LinkedList();

	void setFirst(Link* first);
	void setLast(Link* last);
	void setSize(int size);
	Link* getFirst();
	Link* getLast();
	int getSize();

	void cleanup();
	void add(int data);
	void remove(int data);
	Link* search(int data);
	void print();
};


#endif