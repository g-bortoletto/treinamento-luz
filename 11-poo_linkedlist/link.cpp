#include "link.h"

template<class T>
Link<T>::Link(T data)
{
	setData(data);
	setNext(nullptr);
}

template<class T>
Link<T>::~Link()
{
	setData(0);
	setNext(nullptr);
}

template<class T>
void Link<T>::setData(T data)
{
	m_data = data;
}

template<class T>
void Link<T>::setNext(Link* next)
{
	m_next = next;
}

template<class T>
T Link<T>::getData()
{
	return m_data;
}

template<class T>
Link<T>* Link<T>::getNext()
{
	return m_next;
}

template<class T>
Link<T>* Link<T>::operator=(Link<T>* other)
{
	setData(other->getData());
	setNext(other->getNext());

	return this;
}
