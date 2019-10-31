namespace LearningCSharp
{
    public partial class RPNCalculatorEngine
    {
        protected struct InputLine
        {
            private string _mantissa;
            private bool _isNegative;
            private string _exponent;
            private bool _expIsNegative;
            private bool _inExponent;
            private bool _isActive;

            public bool IsActive { get { return _isActive; } }

            public override string ToString()
            {
                string mantissa = (_isNegative ? "-" : "") + _mantissa;
                string exponent = _inExponent ? "e" + (_expIsNegative ? "-" : "+") + _exponent : "";
                return mantissa + exponent;
            }

            public double ToDouble()
            {
                return double.Parse(this.ToString());
            }
            
            public void Clear()
            {
                _mantissa = null;
                _exponent = null;
                _isNegative = false;
                _expIsNegative = false;
                _inExponent = false;
                _isActive = false;
            }

            public bool AddChar(char inputCharacter)
            {
                if (inputCharacter >= '0' && inputCharacter <= '9')
                {
                    if (_inExponent)
                    {
                        _exponent += inputCharacter;
                    }
                    else
                    {
                        _mantissa += inputCharacter;
                    }
                }
                else
                {
                    switch (inputCharacter)
                    {
                        case '.':
                            if (!_inExponent && !_mantissa.Contains("."))
                            {
                                _mantissa += inputCharacter;
                            }
                            break;

                        case '_':
                            if (_inExponent)
                            {
                                _expIsNegative = !_expIsNegative;
                            }
                            else if (_isActive)
                            {
                                _isNegative = !_isNegative;
                            }
                            else
                            {
                                return false;
                            }
                            break;

                        case 'E':
                            _inExponent = true;
                            break;

                        case '\b':
                            if (_inExponent)
                            {
                                if (_exponent.Length > 0)
                                {
                                    _exponent = _exponent.Remove(_exponent.Length - 1);
                                }
                                else
                                {
                                    _inExponent = false;
                                }
                                return true;
                            }
                            else if (_isActive)
                            {
                                if (_mantissa.Length > 0)
                                {
                                    _mantissa = _mantissa.Remove(_mantissa.Length - 1);
                                }
                                else
                                {
                                    _isActive = false;
                                    return false;
                                }
                                return true;
                            }
                            return false;
                            
                        default:
                            return false;
                    }
                }
                _isActive = true;
                return true;
            }
        }
    }
}
