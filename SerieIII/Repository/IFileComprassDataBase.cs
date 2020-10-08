﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huffman;

namespace LABREPO_ED2.Repository
{
    public interface IFileComprassDataBase
    {
        Stack<FileCompress> GetFiles();

        //conexion the method in class
        void AddNewFile(string nfo, string pnf, float rc, float fc, float rp, string alg);
    }
}