using System.Collections;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _marks;   //точки по яким їде авто

    private IEnumerator _corutine;
    private Vector3 _targetPosition;
    private Vector3 _rotateVector;
    private int _markIndex;

    private void Start()
    {
        _targetPosition = new Vector3();
        _rotateVector = new Vector3();
        _markIndex = 0;
        _targetPosition = _marks[_markIndex].position;

        _markIndex++;

        _corutine = Move();
        StartCoroutine(_corutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        //якщо прийшов до цільової точки
        if (other.GetComponent<Mark>() != null)
        {
            _targetPosition = _marks[_markIndex].position;

            _markIndex++;

            if (_markIndex >= _marks.Length)
                _markIndex = 0;
        }
    }

    private IEnumerator Move()
    {
        while (true)
        {
            _rotateVector = _targetPosition;
            _rotateVector.y = transform.position.y;
            transform.LookAt(_rotateVector);

            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

            yield return new WaitForFixedUpdate();
        }
    }
}