using UnityEngine;
using System.Collections;

public class permeability : MonoBehaviour
{
	public bool m_throughTop;
	public bool m_throughBottom;
	public bool m_throughLeft;
	public bool m_throughRight;
	public bool getUp ()
	{
		return m_throughTop;
	}
	public bool getDown ()
	{
		return m_throughBottom;
	}
	public bool getLeft ()
	{
		return m_throughLeft;
	}
	public bool getRight ()
	{
		return m_throughRight;
	}
	//For legacy reasons
	public bool m_horizontal; 
}
