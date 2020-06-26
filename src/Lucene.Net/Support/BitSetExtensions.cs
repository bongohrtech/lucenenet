/*
 *
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 *
*/

using System;
using System.Collections;
using J2N.Collections;
using Lucene.Net.Util;

namespace Lucene.Net.Support
{
    /// <summary>
    /// This class provides supporting methods of java.util.BitSet
    /// that are not present in System.Collections.BitSet.
    /// </summary>
    internal static class BitSetExtensions
    {
        public static BitSet And_UnequalLengths(this BitSet bitsA, BitSet bitsB)
        {
            //Cycle only through fewest bits neccessary without requiring size equality
            var maxIdx = Math.Min(bitsA.Length, bitsB.Length);//exclusive
            var bits = new BitSet(maxIdx);
            for (int i = 0; i < maxIdx; i++)
            {
                bits.Set(i, bitsA.Get(i) & bitsB.Get(i));
            }
            return bits;
        }

        // Produces a bitwise-or of the two BitSets without requiring they be the same length
        public static BitSet Or_UnequalLengths(this BitSet bitsA, BitSet bitsB)
        {
            var shorter = bitsA.Length < bitsB.Length ? bitsA : bitsB;
            var longer = bitsA.Length >= bitsB.Length ? bitsA : bitsB;
            var bits = new BitSet(longer.Length);
            for (int i = 0; i < longer.Length; i++)
            {
                if (i >= shorter.Length)
                {
                    bits.Set(i, longer.Get(i));
                }
                else
                {
                    bits.Set(i, shorter.Get(i) & longer.Get(i));
                }
            }

            return bits;
        }

        // Produces a bitwise-xor of the two BitSets without requiring they be the same length
        public static BitSet Xor_UnequalLengths(this BitSet bitsA, BitSet bitsB)
        {
            var shorter = bitsA.Length < bitsB.Length ? bitsA : bitsB;
            var longer = bitsA.Length >= bitsB.Length ? bitsA : bitsB;
            var bits = new BitSet(longer.Length);
            for (int i = 0; i < longer.Length; i++)
            {
                if (i >= shorter.Length)
                {
                    bits.Set(i, longer.Get(i));
                }
                else
                {
                    bits.Set(i, shorter.Get(i) ^ longer.Get(i));
                }
            }

            return bits;
        }

        // Clears all bits in this BitSet that correspond to a set bit in the parameter BitSet
        public static void AndNot(this BitSet bitsA, BitSet bitsB)
        {
            //Debug.Assert(bitsA.Length == bitsB.Length, "BitSet lengths are not the same");
            for (int i = 0; i < bitsA.Length; i++)
            {
                //bitsA was longer than bitsB
                if (i >= bitsB.Length)
                {
                    return;
                }
                if (bitsA.Get(i) && bitsB.Get(i))
                {
                    bitsA.Set(i, false);
                }
            }
        }

        //Does a deep comparison of two BitSets
        public static bool BitWiseEquals(this BitSet bitsA, BitSet bitsB)
        {
            if (bitsA == bitsB)
                return true;
            if (bitsA.Length != bitsB.Length)
                return false;

            for (int i = 0; i < bitsA.Length; i++)
            {
                if (bitsA.Get(i) != bitsB.Get(i))
                    return false;
            }

            return true;
        }

        //Compares a BitSet with an OpenBitSet
        public static bool Equal(this BitSet a, OpenBitSet b)
        {
            var BitSetCardinality = a.Cardinality;
            if (BitSetCardinality != b.Cardinality())
                return false;

            for (int i = 0; i < BitSetCardinality; i++)
            {
                if (a.Get(i) != b.Get(i))
                    return false;
            }

            return true;
        }
    }
}