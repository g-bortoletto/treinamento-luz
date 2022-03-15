#include "stack.h"

template<class T>
Stack<T>::Stack()
{

}

template<class T>
Stack<T>::~Stack()
{
	Node<T>* node = m_top;
	Node<T>* previous = nullptr;
	while (node)
	{
		previous = node;
		node = node->getNext();
		if (previous)
		{
			delete previous;
		}
	}
}

template<class T>
void Stack<T>::setTop(const Node<T>* top)
{

}

template<class T>
Node<T>* Stack<T>::getTop() const
{

}

template<class T>
void Stack<T>::setSize(const unsigned size)
{

}

template<class T>
unsigned Stack<T>::getSize() const
{

}

template<class T>
bool Stack<T>::push(Node<T>* node)
{

}

template<class T>
bool Stack<T>::pop()
{

}

template<class T>
void Stack<T>::print()
{

}