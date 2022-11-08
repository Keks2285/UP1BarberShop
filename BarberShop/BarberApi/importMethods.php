<?php

function importEmploye($connect){
    if(move_uploaded_file($_FILES['Employers']['tmp_name'], '../files/'.$_FILES['Employers']['name'])){
     $responce=[
         "status"=>false,
         "message"=>"file not found"
     ];
    }

      if (!file_exists('../files/'.$_FILES['Employers']['name'])) {
          $responce=[
              "status"=>false,
              "message"=>"file not found"
          ];
          echo json_encode($responce); die();
      }
     try{
         $file =fopen("../files/".$_FILES['Employers']['name'], 'r');     
         $counter =0;       
         while (!feof($file)){
             $employer = fgetcsv($file,1024 ,';');
                //print_r( $employer);
             //print_r($employer);die();
             //$i = count($employer);
             if ($employer[1]!=null){
                 $createEmployee=$connect->prepare(
                     "insert into Employe( FirstName, LastName, MiddleName, Email, Password, INN, Post_ID, Status_ID) VALUES (?,?,?,?,?,?,?,?)");
                    // print_r($employer);
                     //if(empty($data["middlename"])) $data["middlename"]="-";
                     $createEmployee->execute(array(
                         strval($employer[1]), //firstName
                         strval($employer[2]), //lastname
                         strval($employer[3]), //middlename
                         strval($employer[4]),  //email
                         strval($employer[5]), //password
                         strval($employer[6]), //inn
                         $employer[7], //post_id
                         $employer[8] //status_id
                     ));


                     //print_r($employer); die();
                     $counter++;

             }
         }
         $responce=[
             "status"=>true,
             "message"=>"$counter user imported"
         ];
         echo json_encode($responce);
     } catch(PDOException  $e){
         $responce=[
             "status"=>false,
             "message"=>"employers are not imported"
         ];
         echo json_encode($responce);
     }
     fclose($file);
   //  unlink('../files/'.$_FILES['Employers']['name']);
 }
//////////////



function executeBackup($connect){
    // нужно будет доделать
   // echo "awertyuik";
    try{
            if(move_uploaded_file($_FILES['Employers']['tmp_name'], '../files/'.$_FILES['Employers']['name'])){
                $responce=[
                "status"=>false,
                    "message"=>"file not found"
                ];
            }
            if(move_uploaded_file($_FILES['Reqords']['tmp_name'], '../files/'.$_FILES['Reqords']['name'])){
            $responce=[
                "status"=>false,
                "message"=>"file not found"
                ];
            }
            
          //  echo "fjreiohgerg";
        //print_r($_FILES); die();
            $deleteEmploye=$connect->prepare(
                "DELETE FROM `Employe` WHERE 1");
                $deleteEmploye->execute();

            $deleteStatus=$connect->prepare(
                "DELETE FROM `Status_Employee` WHERE 1");
                $deleteStatus->execute();
            $createStatus=$connect->prepare(
                    "insert into Status_Employee(ID_Status, Name_Status) values (1,'Работает'),(2,'Уволен'),(3,'В отпуске'),(4,'На больничном');");
                    $createStatus->execute();


                    $file =fopen("../files/".$_FILES['Employers']['name'], 'r');   
                   // $arrayEmployers=array();
                   // $i<=count($employer = fgetcsv($file,2048 ,';'));
                    while (!feof($file)){
                         $employer = fgetcsv($file,2048 ,';');
                    
                           //print_r( $employer[0]);die();
                        //print_r($_FILES);
                        //$i = count($employer);
                        if ($employer[1]!=null){
                            //$id=$employer[0];
                            //print_r( $id);die();
                            $createEmployee=$connect->prepare(
                                "insert into Employe(ID_Employee, FirstName, LastName, MiddleName, Email, Password, INN, Post_ID, Status_ID) VALUES (?,?,?,?,?,?,?,?,?)");
                                //print_r($employer);
                                //if(empty($data["middlename"])) $data["middlename"]="-";
                                $createEmployee->execute(array(
                                    $employer[0], //id
                                    strval($employer[1]), //firstName
                                    strval($employer[2]), //lastname
                                    strval($employer[3]), //middlename
                                    strval($employer[4]),  //email
                                    strval($employer[5]), //password
                                    strval($employer[6]), //inn
                                    $employer[7], //post_id
                                    $employer[8] //status_id
                                ));
           
           
                                //print_r($employer); die();
                               
           
                        }
                    }
                
    } catch (PDOException  $e){

    }
}