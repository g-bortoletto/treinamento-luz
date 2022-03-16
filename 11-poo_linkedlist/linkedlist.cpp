#include "linkedlist.h"

#include <stdio.h>

LinkedList::LinkedList()
{
	setFirst(nullptr);
	setLast(nullptr);
	setSize(0);
}

LinkedList::~LinkedList()
{
	cleanup();
}

void LinkedList::setFirst(Link *first)
{
	m_first = first;
}

void LinkedList::setLast(Link *last)
{
	m_last = last;
}

void LinkedList::setSize(int size)
{
	m_size = size;
}

Link *LinkedList::getFirst()
{
	return m_first;
}

Link *LinkedList::getLast()
{
	return m_last;
}

int LinkedList::getSize()
{
	return m_size;
}

void LinkedList::cleanup()
{
	if (getSize() > 0)
	{
		Link *previous = nullptr;
		Link *current = getFirst();

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

void LinkedList::add(int data)
{
	if (!this)
	{
		return;
	}

	Link *newLink = new Link(data);
	Link *link = getFirst();
	Link *previous = nullptr;

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

void LinkedList::remove(int data)
{
	Link *link = getFirst();
	Link *previous = nullptr;

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

Link *LinkedList::search(int data)
{
	Link *found = 0;
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

void LinkedList::print()
{
	Link *link = getFirst();
	if (!link)
	{
		return;
	}
	do
	{
		fprintf(stdout, "%d ", link->getData());
		link = link->getNext();
	} while (link);
	fprintf(stdout, "\n");
}
