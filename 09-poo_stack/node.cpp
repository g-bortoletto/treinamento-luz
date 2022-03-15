#include "node.h"

template<class T>
Node<T>::Node(T data, Node<T>* next = nullptr)
{
	m_data = data;
	m_next = next;
}

template<class T>
Node<T>::~Node()
{
	m_data = 0;
	m_next = nullptr;
}

template<class T>
void Node<T>::setData(const T data)
{
	m_data = data;
}

template<class T>
T Node<T>::getData() const
{
	return m_data;
}

template<class T>
void Node<T>::setNext(const Node<T>* next)
{
	m_next = next;
}

template<class T>
Node<T>* Node<T>::getNext() const
{
	return m_next;
}