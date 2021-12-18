import React, { useState, useCallback, Fragment } from "react";

import ReactFlow, {
  addEdge,
  isEdge,
  isNode,
  Background,
  Controls,
  MiniMap,
  removeElements,
} from "react-flow-renderer";

import CustomEdge from "../components/CustomEdge";

import localforage from "localforage";

localforage.config({
  name: "react-flow-docs",
  storeName: "flows",
});

const flowKey = "example-flow";

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

const edgeTypes = {
  custom: CustomEdge,
};

const GraphScreen = () => {
  const [rfInstance, setRfInstance] = useState(null);
  const [elements, setElements] = useState(initialElements);
  const [name, setName] = useState("");
  const [myEdges, setMyEdges] = useState([]);
  const [myNodes, setMyNodes] = useState([]);

  const onPrintElements = () => {
    elements.forEach((element) => {
      if (isNode(element)) myNodes.push(element);
      if (isEdge(element)) myEdges.push(element);
    });
    console.log("Nodes: ", myNodes);
    console.log("Edges: ", myEdges);
  };

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

  const onSave = useCallback(() => {
    if (rfInstance) {
      const flow = rfInstance.toObject();
      localforage.setItem(flowKey, flow);
      console.log(localforage.getItem(flowKey));
    }
  }, [rfInstance]);

  const onRestore = useCallback(() => {
    const restoreFlow = async () => {
      const flow = await localforage.getItem(flowKey);
      console.log(flow);

      if (flow) {
        const [x = 0, y = 0] = flow.position;
        setElements(flow.elements || []);
      }
    };

    restoreFlow();
  }, [setElements]);

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

  const onConnect = (params) =>
    setElements((els) => {
      const edge = {
        ...params,
        arrowHeadType: "arrow",
      };
      return addEdge(edge, els);
    });

  return (
    <Fragment>
      <ReactFlow
        elements={elements}
        onLoad={setRfInstance}
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
        edgeTypes={edgeTypes}
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
        <button type="button" onClick={onSave} style={{ marginLeft: 1 }}>
          Save graph
        </button>
        <button type="button" onClick={onRestore} style={{ marginLeft: 1 }}>
          Restore graph
        </button>
        <button
          type="button"
          onClick={onPrintElements}
          style={{ marginLeft: 1 }}
        >
          print elements
        </button>
      </div>
    </Fragment>
  );
};

export default GraphScreen;
