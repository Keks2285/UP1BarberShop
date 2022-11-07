<?php


function getPosts($connect){
    try{
    $searchPosts=$connect->prepare("SELECT ID_Post as 'Id', Name_Post as 'Name', Price from `Post`;");
    $searchPosts->execute();
    $listUser=$searchPosts->fetchAll();

    echo json_encode($listUser);
 } catch (PDOException $e){
    http_response_code(404);
 }

}



function getEmployeByEmail ($connect, $data){
    try{
        $selectEmloyers=$connect->prepare("Select * from Employe where Email=?");
        $selectEmloyers->execute(array(strval($data["email"])));
        if(count($selectEmloyers->fetchAll())<0){
            $responce=[
                "status"=>false,
                "message"=>"Employe doesn't Exist",
                "code"=>404
            ];
            echo json_encode($responce);
            die();
        } else{
            $responce=[
                "status"=>true,
                "message"=>"Employe was founded",
                "code"=>200
            ];
            echo json_encode($responce);
            die();
        }

    } catch (Exception $e){
        $responce=[
            "status"=>false,
            "message"=>"Database Error",
            "code"=>500
        ];
        echo json_encode($responce);
        die();
    }


}

function getConsumptions($connect){
    try{
        $searchPosts=$connect->prepare("SELECT * from Consumption");
        $searchPosts->execute();
        $listUser=$searchPosts->fetchAll();
    
        echo json_encode($listUser);
     } catch (PDOException $e){
        http_response_code(404);
     }
    
}

function getIncomes($connect){
    try{
        $search=$connect->prepare("SELECT * from Income");
        $search->execute();
        $listUser=$search->fetchAll();
    
        echo json_encode($listUser);
     } catch (PDOException $e){
        http_response_code(404);
     }
    
}

function getSupplies($connect){
    try{
        $search=$connect->prepare("SELECT * from Supply");
        $search->execute();
        $listUser=$search->fetchAll();
    
        echo json_encode($listUser);
     } catch (PDOException $e){
        http_response_code(404);
     }
    
}

function getSickLeaves($connect){
    try{
        $search=$connect->prepare("SELECT * from SickLeave");
        $search->execute();
        $listUser=$search->fetchAll();
    
        echo json_encode($listUser);
     } catch (PDOException $e){
        http_response_code(404);
     }
    
}

function getVacations($connect){
    try{
        $search=$connect->prepare("SELECT * from Vacation");
        $search->execute();
        $listUser=$search->fetchAll();
    
        echo json_encode($listUser);
     } catch (PDOException $e){
        http_response_code(404);
     }
    
}

function getClientByEmail ($connect, $data){
    try{
        $selectEmloyers=$connect->prepare("Select * from Client where Email=?");
        $selectEmloyers->execute(array(strval($data["email"])));
        if(count($selectEmloyers->fetchAll())<0){
            $responce=[
                "status"=>false,
                "message"=>"Client doesn't Exist",
                "code"=>404
            ];
            echo json_encode($responce);
            die();
        } else{
            $responce=[
                "status"=>true,
                "message"=>"Client was founded",
                "code"=>200
            ];
            echo json_encode($responce);
            die();
        }

    } catch (Exception $e){
        $responce=[
            "status"=>false,
            "message"=>"Database Error",
            "code"=>500
        ];
        echo json_encode($responce);
        die();
    }

}

function getClients ($connect){
    try{
        $select=$connect->prepare("Select * from Client");
        $select->execute();
        $list=$select->fetchAll();
        echo json_encode($list);
    } catch (Exception $e){
        $responce=[
            "status"=>false,
            "message"=>"Database Error",
            "code"=>500
        ];
        echo json_encode($responce);
        die();
    }

}

function getRecords ($connect){
    try{
        $select=$connect->prepare("Select * from Record");
        $select->execute();
        $list=$select->fetchAll();
        echo json_encode($list);
    } catch (Exception $e){
        $responce=[
            "status"=>false,
            "message"=>"Database Error",
            "code"=>500
        ];
        echo json_encode($responce);
        die();
    }

}

function getMaterials ($connect){
    try{
        $select=$connect->prepare("Select * from Material");
        $select->execute();
        $list=$select->fetchAll();
        echo json_encode($list);
    } catch (Exception $e){
        $responce=[
            "status"=>false,
            "message"=>"Database Error",
            "code"=>500
        ];
        echo json_encode($responce);
        die();
    }

}

function getEmployers($connect){
    try{
    $searchUser=$connect->prepare("SELECT ID_Employee, ID_Status, ID_Post, Password, FirstName, LastName, MiddleName, Email, INN, Name_Post, Name_Status FROM `Employe` join Post join Status_Employee where Post_ID=ID_Post and Status_ID=ID_Status;");
    $searchUser->execute();
    $listUser=$searchUser->fetchAll();

    echo json_encode($listUser);
 } catch (PDOException $e){
    http_response_code(404);
 }

}


function getStatuses($connect){
    try{
    $search=$connect->prepare("SELECT * from `Status_Employee`;");
    $search->execute();
    $listUser=$search->fetchAll();

    echo json_encode($listUser);
 } catch (PDOException $e){
    http_response_code(404);
 }

}

function getServices($connect){
    try{
    $search=$connect->prepare("SELECT * from `Service`;");
    $search->execute();
    $listUser=$search->fetchAll();

    echo json_encode($listUser);
 } catch (PDOException $e){
    http_response_code(404);
 }

}

function getStocks($connect){
    try{
    $search=$connect->prepare("SELECT * from `Stock`;");
    $search->execute();
    $listUser=$search->fetchAll();

    echo json_encode($listUser);
 } catch (PDOException $e){
    http_response_code(404);
 }

}

function getTaxReports($connect){
    try{
    $search=$connect->prepare("SELECT * from `TaxReport`;");
    $search->execute();
    $listUser=$search->fetchAll();

    echo json_encode($listUser);
 } catch (PDOException $e){
    http_response_code(404);
 }

}


function getProviders($connect){
    try{
        $search=$connect->prepare("SELECT * from `Provider`;");
        $search->execute();
        $listUser=$search->fetchAll();
    
        echo json_encode($listUser);
     } catch (PDOException $e){
        http_response_code(404);
     }
}
