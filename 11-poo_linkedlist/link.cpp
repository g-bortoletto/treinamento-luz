#include "link.h"

Link::Link(int data)
{
	setData(data);
	setNext(nullptr);
}

Link::~Link()
{
	setData(0);
	setNext(nullptr);
}

void Link::setData(int data)
{
	m_data = data;
}

void Link::setNext(Link* next)
{
	m_next = next;
}

int Link::getData()
{
	return m_data;
}

Link* Link::getNext()
{
	return m_next;
}

Link* Link::operator=(Link* other)
{
	setData(other->getData());
	setNext(other->getNext());

	return this;
}
