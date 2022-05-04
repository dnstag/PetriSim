// Copyright (c) 2022 Yannick Seibert.
// This file is part of PetriSim.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3.
// 
// This program is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.

namespace PetriSim.DS;

public readonly struct Coords
{
    public Coords(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; }
    public double Y { get; }

    public override string ToString() => $"({X}, {Y})";
}

public abstract class Node
{
    private readonly List<Node> _dst;
    private readonly List<Node> _src;
    private string? _id;

    protected Node(string id)
    {
        Id = id;
        Label = "";
        _src = new List<Node>();
        _dst = new List<Node>();
    }

    public string? Id
    {
        get => _id;
        set => _id = value ?? throw new ArgumentNullException(nameof(value), "Id must not be null.");
    }
    
    public string Label { get; set; }
    public Coords Coordinates { get; set; }

    public void AddDestination(Node node)
    {
        if (_dst.Exists(n => n.Equals(node))) return;

        _dst.Add(node);
        node.AddSource(this);
    }

    private void AddSource(Node node)
    {
        if (_src.Exists(n => n.Equals(node))) return;

        _src.Add(node);
    }

    public Node[] GetDestinations()
    {
        return _dst.ToArray();
    }

    public Node[] GetSources()
    {
        return _src.ToArray();
    }
}