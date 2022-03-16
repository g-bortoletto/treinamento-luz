#include "linkedlist.h"

#include <stdio.h>

template<class T>
LinkedList<T>::LinkedList()
{
	setFirst(nullptr);
	setLast(nullptr);
	setSize(0);
}

template<class T>
LinkedList<T>::~LinkedList()
{
	cleanup();
}

template<class T>
void LinkedList<T>::setFirst(Link<T> *first)
{
	m_first = first;
}

template<class T>
void LinkedList<T>::setLast(Link<T> *last)
{
	m_last = last;
}

template<class T>
void LinkedList<T>::setSize(int size)
{
	m_size = size;
}

template<class T>
Link<T> *LinkedList<T>::getFirst()
{
	return m_first;
}

template<class T>
Link<T> *LinkedList<T>::getLast()
{
	return m_last;
}

template<class T>
T LinkedList<T>::getSize()
{
	return m_size;
}

template<class T>
void LinkedList<T>::cleanup()
{
	if (getSize() > 0)
	{
		Link<T> *previous = nullptr;
		Link<T> *current = getFirst();

		while (current)
		{
			previous = current;
			if (!previous)
			{
				delete current;
			}
			else
			{
				current = current->getNext();
				delete previous;
			}
		}
	}
	setFirst(nullptr);
	setLast(nullptr);
	setSize(0);
}

template<class T>
void LinkedList<T>::add(T data)
{
	if (!this)
	{
		return;
	}

	Link<T> *newLink = new Link<T>(data);
	Link<T> *link = getFirst();
	Link<T> *previous = nullptr;

	while (link)
	{
		previous = link;
		link = link->getNext();
	}
	link = newLink;

	if (previous)
	{
		previous->setNext(link);
	}
	else
	{
		setFirst(link);
	}
	setLast(link);
	setSize(getSize() + 1);
}

template<class T>
void LinkedList<T>::remove(T data)
{
	Link<T> *link = getFirst();
	Link<T> *previous = nullptr;

	while (link)
	{
		if (link->getData() == data)
		{
			if (previous)
			{
				previous->setNext(link->getNext());
				if (!previous->getNext())
				{
					setLast(previous);
				}
			}
			else
			{
				setFirst(link->getNext());
			}
			delete link;
			setSize(getSize() - 1);
		}
		previous = link;
		link = link->getNext();
	}
}

template<class T>
Link<T> *LinkedList<T>::search(T data)
{
	Link<T> *found = 0;
	int i;

	found = getFirst();
	for (i = 0; i < getSize(); ++i)
	{
		if (found->getData() == data)
		{
			return found;
		}
		found = found->getNext();
	}
	found = nullptr;

	return found;
}

template<class T>
void LinkedList<T>::print()
{
	Link<T> *link = getFirst();
	if (!link)
	{
		return;
	}
	do
	{
		fprintf(stdout, "%c ", link->getData());
		link = link->getNext();
	} while (link);
	fprintf(stdout, "\n");
}
