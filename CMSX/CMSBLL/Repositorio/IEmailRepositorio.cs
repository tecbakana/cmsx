﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace CMSBLL.Repositorio
{
    public interface IEmailRepositorio
    {
        void MontaEmail();
        void Enviar();
    }
}