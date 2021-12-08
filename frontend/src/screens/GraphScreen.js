import React, { useState, Fragment } from "react";

import ReactFlow, {
  addEdge,
  Background,
  Controls,
  MiniMap,
  removeElements,
} from "react-flow-renderer";

const initialElements = [
  {
    id: "1",
    type: "input",
    data: { label: "Mind Node" },
    position: { x: 0, y: 0 },
  },
];
const onLoad = (reactFlowInstance) => {
  reactFlowInstance.fitView();
};

const GraphScreen = () => {
  const [elements, setElements] = useState(initialElements);
  const [name, setName] = useState("");

  const addNode = () => {
    setElements((e) =>
      e.concat({
        id: (e.length + 1).toString(),
        data: { label: `${name}` },
        position: {
          x: Math.random() * window.innerWidth,
          y: Math.random() * window.innerHeight,
        },
      })
    );
    console.log(elements);
  };

  // Node position update after draging
  const onNodeDragStop = (event, node) => {
    let elementsCopy = elements;
    let index = elements.findIndex((element) => element.id === node.id);
    let newPositionNode = elements[index];
    newPositionNode.position = node.position;
    elementsCopy.splice(index, 1, newPositionNode);
    setElements(elementsCopy);
  };

  const onElementsRemove = (elementsToRemove) =>
    setElements((els) => removeElements(elementsToRemove, els));

  const onConnect = (params) => setElements((els) => addEdge(params, els));

  return (
    <Fragment>
      <ReactFlow
        elements={elements}
        onLoad={onLoad}
        style={{ width: "100%", height: "80vh", border: "1px solid #16001E" }}
        onConnect={onConnect}
        connectionLineStyle={{ stroke: "#ddd", strokeWidth: 2 }}
        connectionLineType="bezier"
        snapToGrid={true}
        snapGrid={[16, 16]}
        onElementsRemove={onElementsRemove}
        deleteKeyCode={46}
        selectionKeyCode={17}
        onNodeDragStop={onNodeDragStop}
        getConnectedEdges
      >
        <Background color="#888" gap={16} />
        <MiniMap
          style={{ border: "1px solid #16001E" }}
          nodeColor={(n) => {
            if (n.type === "input") return "blue";

            return "#FFCC00";
          }}
        />
        <Controls />
      </ReactFlow>

      <div>
        <input
          type="text"
          onChange={(e) => setName(e.target.value)}
          name="title"
        />
        <button type="button" onClick={addNode}>
          Add Node
        </button>
      </div>
    </Fragment>
  );
};

export default GraphScreen;
