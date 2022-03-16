#ifndef LINKEDLIST_H
#define LINKEDLIST_H


#include "link.h"

template<class T>
class LinkedList
{
private:
	Link<T>* m_first;
	Link<T>* m_last;
	int   m_size;

public:
	LinkedList();
	~LinkedList();

	void setFirst(Link<T>* first);
	void setLast(Link<T>* last);
	void setSize(int size);
	Link<T>* getFirst();
	Link<T>* getLast();
	T getSize();

	void cleanup();
	void add(T data);
	void remove(T data);
	Link<T>* search(T data);
	void print();
};


#endif